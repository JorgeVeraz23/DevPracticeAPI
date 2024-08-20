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
    public class ReservaRepository : ReservaInterface
    {
        private readonly ApplicationDbContext _context;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly string _username;
        private readonly string _ip;

        public ReservaRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
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


        public async Task<MessageInfoDTO> ActualizarReserva(ReservaDTO reserva)
        {
            try
            {
                var model = await _context.Reservas.Where(x => x.Active && x.IdReserva == reserva.IdReserva).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la reserva");

                model.IdVehiculo = reserva.IdVehiculo;
                model.FechaReserva = reserva.FechaReserva;
                model.ClienteNombre = reserva.ClienteNombre;
                model.ClienteTelefono = reserva.ClienteTelefono;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                infoDTO.AccionCompletada("Se ha actualizado la reserva");
                return infoDTO;
            }
            catch(Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "ReservaRepository", "Error al intentar actualizar la reserva");
            }

        }

        public async Task<MessageInfoDTO> CrearReserva(ReservaDTO reserva)
        {
            try
            {
                Reserva reservaEntity = new Reserva();
                reservaEntity.Active = true;
                reservaEntity.IdVehiculo = reserva.IdVehiculo;
                reservaEntity.FechaReserva = reserva.FechaReserva;
                reservaEntity.ClienteTelefono = reserva.ClienteTelefono;
                reservaEntity.ClienteNombre = reserva.ClienteNombre;

                reservaEntity.DateRegister = DateTime.Now;
                reservaEntity.UserRegister = _username;
                reservaEntity.IpRegister = _ip;

                await _context.Reservas.AddAsync(reservaEntity);
                await _unitOfWorkRepository.SaveChangesAsync();

                // Mueve la llamada a EstablecerVehiculoNoDisponible antes del return
                await EstablecerVehiculoNoDisponible(reserva.IdVehiculo);

                infoDTO.AccionCompletada("Se ha creado la reserva");
                return infoDTO;


            }catch(Exception ex)
            {
                
                return infoDTO.AccionFallida("Ocurrio un error al crear la reserva", 400) ; 
            }
        }

        public async  Task<MessageInfoDTO> EliminarReserva(long idReserva)
        {
            var reservaToDelete = await _context.Reservas.Where(x =>x.Active && x.IdReserva  == idReserva).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la reserva");

            reservaToDelete.Active = false;
            reservaToDelete.DateDelete = DateTime.Now;
            reservaToDelete.UserDelete = _username;
            reservaToDelete.IpDelete = _ip;

            await _unitOfWorkRepository.SaveChangesAsync();

            infoDTO.AccionCompletada("la reserva seleccionada a sido eliminada");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> EstablecerVehiculoNoDisponible(long idVehiculo)
        {
            var vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(x => x.Active && x.IdVehiculo == idVehiculo) ?? throw new Exception("Ocurrio un error al buscar el vehiculo");

            vehiculo.Disponible = false;

            vehiculo.DateModification = DateTime.Now;
            vehiculo.UserModification = _username;
            vehiculo.IpModification = _ip;
            await _unitOfWorkRepository.SaveChangesAsync();

            infoDTO.AccionCompletada("Se a actualizado el estado del vehiculo");

            return infoDTO;

            
        }

        public Task<ReservaDTO> ObtenerReservaPorId(long idReserva)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MostrarReservaDto>> ObtenerTodasLasReservas()
        {


            var reservas = await _context.Reservas.Where(x => x.Active).Select(c => new MostrarReservaDto
            {
                IdReserva = c.IdReserva,
                VehiculoVirtual = c.Vehiculo ,
                FechaReserva = c.FechaReserva,
                ClienteNombre = c.ClienteNombre,
                ClienteTelefono = c.ClienteTelefono,

            }).ToListAsync();

            return reservas;
        }
    }
}
