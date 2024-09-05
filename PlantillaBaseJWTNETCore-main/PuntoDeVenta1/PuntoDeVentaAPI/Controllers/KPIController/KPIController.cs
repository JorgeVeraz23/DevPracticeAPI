using ClosedXML.Excel;
using Data.Dto.KPIDto;
using Data.Entities.KPIEntity;
using Data.Interfaces.IKPIRepository;
using Data.Repository.KPIRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace PuntoDeVentaAPI.Controllers.KPIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIController : ControllerBase
    {

        private readonly IKPIRepository _repository;

        public KPIController(IKPIRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("descargarExcel")]
        public async Task<IActionResult> DescargarExcel()
        {
            var kpis = await _repository.obtenerKPIAsync();

            using(var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("KPIs");

                //Crear encabezados
                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 2).Value = "Valor";
                worksheet.Cell(1, 3).Value = "Fecha";

                int currentRow = 2;

                foreach(var kpi in kpis)
                {
                    worksheet.Cell(currentRow, 1).Value = kpi.Nombre;
                    worksheet.Cell(currentRow, 2).Value = kpi.Valor;
                    worksheet.Cell(currentRow, 2).Value = kpi.Fecha.ToString("yyyy-MM-dd");
                    currentRow++;
                }

                using(var stream = new MemoryStream())
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




        //Con CLOSEDXML
        [HttpPost("cargarMasivadeArchivoExcelClosedXML")]
        public async Task<IActionResult> cargarMasivadeArchivoExcelClosedXML(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo válido.");
            }

            var kpiDtos = new List<KPICargaMasivaDto>();

            using(var stream = new MemoryStream())
            {
                await archivo.CopyToAsync(stream);

                //Usar ClosedXML para cargar el archivo Excel
                using(var workbook = new XLWorkbook(stream))
                {
                    try
                    {
                        var worksheet = workbook.Worksheet(1);//Utilizar la primera hoja de trabajo
                        var rowCount = worksheet.RowsUsed().Count();

                        for(int row = 2; row <= rowCount; row++) //Comenzar desde la segunda fila para evitar encabezados
                        {
                            var dto = new KPICargaMasivaDto
                            {
                                Nombre = worksheet.Cell(row, 1).GetString(),
                                Valor = decimal.Parse(worksheet.Cell(row, 2).GetString(), CultureInfo.InvariantCulture),
                                Fecha = DateTime.Parse(worksheet.Cell(row, 3).GetString(), CultureInfo.InvariantCulture),
                            };
                            kpiDtos.Add(dto);
                        }
                    }catch(Exception ex)
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
