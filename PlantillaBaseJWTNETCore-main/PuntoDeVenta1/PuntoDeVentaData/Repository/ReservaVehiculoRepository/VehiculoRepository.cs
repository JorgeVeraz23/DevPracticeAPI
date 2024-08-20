
using Data.Dto.ReservaVehiculoDTO;
using Data.Entities.ReservaVehiculos;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Data.Interfaces.SecurityInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.ReservaVehiculoRepository
{
    public class VehiculoRepository : VehiculoInterface
    {
        private readonly ApplicationDbContext _context;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly string _username;
        private readonly string _ip;

        public VehiculoRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
        {

            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unitOfWorkRepository = unitOfWorkRepository;
            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () => (
                await userManager.FindByNameAsync(
                httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""
            )
                )?.UserName ?? "Desconocido").Result;

        }

        public async Task<MessageInfoDTO> ActualizarVehiculo(VehiculoDTO vehiculo)
        {
            try
            {
                var model = await _context.Vehiculos.Where(x => x.Active && x.IdVehiculo == vehiculo.IdVehiculo).FirstOrDefaultAsync() ?? throw new Exception("No se encntro el vehiculo");


                model.Marca = vehiculo.Marca;
                model.Modelo = vehiculo.Modelo;
                model.Anio = vehiculo.Anio;
                model.Disponible = vehiculo.Disponible;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;

            }catch(Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "VehiculoRepository", "Error al intentar actualizar el vehiculo");
            }
        }

        public async Task<MessageInfoDTO> CrearVehiculo(VehiculoDTO vehiculo)
        {
            var ifExist = await _context.Vehiculos.Where(x => x.Active && x.Marca == vehiculo.Marca && x.Modelo == vehiculo.Modelo && x.Anio == vehiculo.Anio).FirstOrDefaultAsync();
            
            if (ifExist != null)
            {
                infoDTO.AccionFallida("Ya existe un vehiculo registrado con esos datos", 400);
                return infoDTO;
            }

            Vehiculo vehiculoEntidad = new Vehiculo();
            vehiculoEntidad.Active = true;
            vehiculoEntidad.Marca = vehiculo.Marca;
            vehiculoEntidad.Modelo = vehiculo.Modelo;
            vehiculoEntidad.Anio = vehiculo.Anio;
            vehiculoEntidad.Disponible = vehiculo.Disponible;
            vehiculoEntidad.DateRegister = DateTime.Now;
            vehiculoEntidad.UserRegister = _username;
            vehiculoEntidad.IpRegister = _ip;

            await _context.Vehiculos.AddAsync(vehiculoEntidad);
            await _unitOfWorkRepository.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado el vehiculo");
            return infoDTO;

        }

        public async Task<MessageInfoDTO> EliminarVehiculo(long idVehiculo)
        {
            var vehiculoToDelete = await _context.Vehiculos.Where(x => x.Active && x.IdVehiculo == idVehiculo).FirstOrDefaultAsync();

            if(vehiculoToDelete == null)
            {
                infoDTO.AccionFallida("El vehiculo a eliminar no se encuentra disponible", 400);   
            }

            vehiculoToDelete!.DateDelete = DateTime.Now;
            vehiculoToDelete.Active = false;
            vehiculoToDelete.UserDelete = _username;
            vehiculoToDelete.IpDelete = _ip;

            await _unitOfWorkRepository.SaveChangesAsync();

            infoDTO.AccionCompletada("El vehiculo a sido eliminado correctamente");

            return infoDTO;

        }

        public async Task<List<VehiculoDTO>> MostrarTodosLosVehiculos()
        {
            var vehiculos = await _context.Vehiculos.Where(x => x.Active).Select(c => new VehiculoDTO
            {
                IdVehiculo = c.IdVehiculo,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Anio = c.Anio,
                Disponible = c.Disponible,
            }).ToListAsync();

            return vehiculos;
        }

        public async Task<VehiculoDTO> ObtenerVehiculoPorId(long idVehiculo)
        {
           
            var vehiculo = await _context.Vehiculos.Where(x => x.Active && x.IdVehiculo == idVehiculo).Select(c => new VehiculoDTO
            {
                IdVehiculo = c.IdVehiculo,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Anio = c.Anio,
                Disponible = c.Disponible,
            }).FirstOrDefaultAsync();

            if(vehiculo == null)
            {
                throw new Exception("El vehiculo no fue encontrado");
            }

            return vehiculo;

            //otra forma de retornar sin el if vehiculo == null
            /*
                return vehiculo ?? new VehiculoDTO();
             */
            

        }
    }
}
