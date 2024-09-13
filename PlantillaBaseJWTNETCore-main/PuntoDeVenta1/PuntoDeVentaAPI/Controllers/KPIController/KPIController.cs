using ClosedXML.Excel;
using Data;
using Data.Dto.KPIDto;
using Data.Entities.DtoExample;
using Data.Entities.KPIEntity;
using Data.Entities.Prueba;
using Data.Interfaces.IKPIRepository;
using Data.Repository.KPIRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace PuntoDeVentaAPI.Controllers.KPIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIController : ControllerBase
    {

        private readonly IKPIRepository _repository;
        private ApplicationDbContext _context;

        public KPIController(IKPIRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }


        [HttpGet("descargarExcel")]
        public async Task<IActionResult> DescargarExcel()
        {
            var kpis = await _repository.obtenerKPIAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("KPIs");

                //Crear encabezados
                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 2).Value = "Valor";
                worksheet.Cell(1, 3).Value = "Fecha";

                int currentRow = 2;

                foreach (var kpi in kpis)
                {
                    worksheet.Cell(currentRow, 1).Value = kpi.Nombre;
                    worksheet.Cell(currentRow, 2).Value = kpi.Valor;
                    worksheet.Cell(currentRow, 2).Value = kpi.Fecha.ToString("yyyy-MM-dd");
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    // Asegurar que el nombre del archivo sea seguro y válido
                    var safeFileName = $"KPIs_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                safeFileName);
                }
            }
        }



        /// <summary>
        /// Carga Masiva con CLOSEDXML
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        //Con CLOSEDXML
        [HttpPost("cargarMasivadeArchivoExcelClosedXML")]
        public async Task<IActionResult> cargarMasivadeArchivoExcelClosedXML(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo válido.");
            }

            var kpiDtos = new List<KPICargaMasivaDto>();

            using (var stream = new MemoryStream())
            {
                await archivo.CopyToAsync(stream);

                //Usar ClosedXML para cargar el archivo Excel
                using (var workbook = new XLWorkbook(stream))
                {
                    try
                    {
                        var worksheet = workbook.Worksheet(1);//Utilizar la primera hoja de trabajo
                        var rowCount = worksheet.RowsUsed().Count();

                        for (int row = 2; row <= rowCount; row++) //Comenzar desde la segunda fila para evitar encabezados
                        {
                            var dto = new KPICargaMasivaDto
                            {
                                Nombre = worksheet.Cell(row, 1).GetString(),
                                Valor = decimal.Parse(worksheet.Cell(row, 2).GetString(), CultureInfo.InvariantCulture),
                                Fecha = DateTime.Parse(worksheet.Cell(row, 3).GetString(), CultureInfo.InvariantCulture),
                            };
                            kpiDtos.Add(dto);
                        }
                    } catch (Exception ex)
                    {
                        return BadRequest($"Error al procesar el archivo Excel: {ex.Message}");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kpis = kpiDtos.Select(dto => new KPIEntity
            {
                Nombre = dto.Nombre,
                Valor = dto.Valor,
                Fecha = dto.Fecha,
            });

            await _repository.CargaMasivaAsync(kpis);

            return Ok("Carga masiva desde archivo Excel completada exitosamente");
        }



        /// <summary>
        /// Carga Masiva Dinamica
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost("CargaMasivaDinamica")]
        public async Task<IActionResult> CargaMasivaDinamica(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo válido.");
            }

            using (var package = new ExcelPackage(archivo.OpenReadStream()))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    var nombreTabla = worksheet.Name;

                    //Verifica que la tabla existe en la base de datos
                    var tipoEntidad = ObtenerTipoEntidadPorNombreTabla(nombreTabla);

                    if (tipoEntidad == null)
                    {
                        continue; //Si la tabla no existe, omite el procesamiento
                    }

                    var datos = ConvertirHojaADatos(worksheet);
                    await GuardarDatosEnTabla(tipoEntidad, datos);
                }
            }

            return Ok("Carga masiva desde archivo Excel completada exitosamente");
        }



        //private Type ObtenerTipoEntidadPorNombreTabla(string nombreTabla)
        //{

        //    Type diccionarioTablas = Type.GetType(nombreTabla);
        //    ////Aqui puedes tener un mapeo de nombres de tablas a tipos de entidades
        //    //var diccionarioTablas = new Dictionary<string, Type>
        //    //{
        //    //    //Aqui mapeamos nombres de tablas a tipos de entidades
        //    //    {"Clientes", typeof(Cliente) },
        //    //    {"Productos",typeof(Product) },
        //    //    {"BOOK", typeof(Book) }
        //    //};

        //    diccionarioTablas.TryGetValue(nombreTabla, out var tipoEntidad);
        //    return tipoEntidad;

        //}

        private Type ObtenerTipoEntidadPorNombreTabla(string nombreTabla)
        {
            // Obtén todas las entidades registradas en el modelo de tu DbContext
            var entidades = _context.Model.GetEntityTypes();

            // Buscar el tipo de entidad basado en el nombre de la tabla
            var entidad = entidades.FirstOrDefault(e => e.GetTableName().Equals(nombreTabla, StringComparison.OrdinalIgnoreCase));

            if (entidad != null)
            {
                // Retornar el CLR Type asociado a la entidad
                return entidad.ClrType;
            }

            // Si no se encuentra ninguna coincidencia, devolver null o manejar el error según sea necesario
            return null;
        }


        private DataTable ConvertirHojaADatos(ExcelWorksheet worksheet)
        {
            var dataTable = new DataTable();

            // Añadir columnas
            for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
                dataTable.Columns.Add(worksheet.Cells[1, i].Text);
            }

            // Añadir filas
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = dataTable.NewRow();
                for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                {
                    row[j - 1] = worksheet.Cells[i, j].Text;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }


        private async Task GuardarDatosEnTabla(Type tipoEntidad, DataTable datos)
        {
            try
            {
                // Obtén la instancia de DbSet usando el método genérico Set<T>
                //MethodInfo method = typeof(ApplicationDbContext).GetMethod("Set").MakeGenericMethod(tipoEntidad).Invoke(_context, null);
                //var dbSet = method.Invoke(_context, null); // _context es tu instancia de DbContext

                var results = _context.GetType().GetMethod("Set", 1, Type.EmptyTypes).MakeGenericMethod(tipoEntidad).Invoke(_context, null);

                // Convierte dbSet a IQueryable para trabajar con datos
                var addMethod = results.GetType().GetMethod("Add");

                // Iterar sobre las filas de la tabla de datos y agregarlas al DbSet
                foreach (DataRow fila in datos.Rows)
                {
                    var entidad = Activator.CreateInstance(tipoEntidad);

                    foreach (DataColumn columna in datos.Columns)
                    {
                        // Asignar el valor de cada columna a la propiedad correspondiente en la entidad
                        PropertyInfo propiedad = tipoEntidad.GetProperty(columna.ColumnName);
                        //if (propiedad != null && fila[columna] != DBNull.Value)
                        //{
                        //    propiedad.SetValue(entidad, Convert.ChangeType(fila[columna], propiedad.PropertyType));
                        //}
                        if (propiedad != null && fila[columna] != DBNull.Value)
                        {
                            object valor = fila[columna];
                            if (valor == null)
                            {
                                valor = "a";
                            }
                            else if (valor is string str && string.IsNullOrEmpty(str))
                            {
                                valor = null;
                            }
                            // Convertir el valor según el tipo de propiedad
                            else if (propiedad.PropertyType == typeof(DateTime?) || propiedad.PropertyType == typeof(DateTime))
                            {
                                // Intentar convertir el valor a DateTime
                                if (DateTime.TryParse(valor.ToString(), out DateTime dateValue))
                                {
                                    valor = dateValue;
                                }
                                else
                                {
                                    // Si no se puede convertir, asignar null si es nullable
                                    valor = propiedad.PropertyType == typeof(DateTime?) ? (DateTime?)null : default(DateTime);
                                }
                            }
                            else
                            {
                                // Convertir a otros tipos
                                valor = Convert.ChangeType(valor, propiedad.PropertyType);
                            }

                            propiedad.SetValue(entidad, valor);

                        }

                        // Agrega la entidad al DbSet
                        addMethod.Invoke(results, new[] { entidad });
                    }

                    // Guarda los cambios en la base de datos
                    await _context.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                throw new Exception($"Error al guardar los datos en la tabla: {ex.Message}", ex);
            }
        }






            //Con EPPLUS
            [HttpPost("cargaMasivaDesdeArchivoExcel")]
            public async Task<IActionResult> CargaMasivaDesdeArchivoExcel(IFormFile archivo)
            {
                if (archivo == null || archivo.Length == 0)
                {
                    return BadRequest("No se ha proporcionado un archivo válido.");
                }

                // Establecer el contexto de licencia de EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var kpiDtos = new List<KPICargaMasivaDto>();

                using (var stream = new MemoryStream())
                {
                    await archivo.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        try
                        {
                            var worksheet = package.Workbook.Worksheets[0]; // Utilizar la primera hoja de trabajo
                            int rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++) // Comenzar desde la segunda fila para evitar encabezados
                            {
                                var dto = new KPICargaMasivaDto
                                {
                                    Nombre = worksheet.Cells[row, 1].Text,
                                    Valor = decimal.Parse(worksheet.Cells[row, 2].Text, CultureInfo.InvariantCulture),
                                    Fecha = DateTime.Parse(worksheet.Cells[row, 3].Text, CultureInfo.InvariantCulture)
                                };
                                kpiDtos.Add(dto);
                            }
                        }
                        catch
                        {
                            return BadRequest("Error al procesar el archivo Excel.");
                        }
                    }
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var kpis = kpiDtos.Select(dto => new KPIEntity
                {
                    Nombre = dto.Nombre,
                    Valor = dto.Valor,
                    Fecha = dto.Fecha
                });

                await _repository.CargaMasivaAsync(kpis);

                return Ok("Carga masiva desde archivo Excel completada exitosamente.");
            }


        }
    }

