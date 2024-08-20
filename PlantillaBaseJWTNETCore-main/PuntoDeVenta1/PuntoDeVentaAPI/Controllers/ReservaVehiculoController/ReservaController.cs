using AutoMapper;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using Data.Dto.ReservaVehiculoDTO;
using System.Net;

namespace PuntoDeVentaAPI.Controllers.ReservaVehiculoController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly ReservaInterface _reservaInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly IConfiguration _confguration;
        private readonly IHttpContextAccessor _httpContextAccesor;

        private readonly ApplicationUserManager _userManager;
        private static Logger _log = LogManager.GetLogger("VehiculoController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;




        public ReservaController(ReservaInterface reservaInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider serviceProvider, IMapper mapper, IConfiguration configuration)
        {
            _reservaInterface = reservaInterface;
            this._context = applicationDbContext;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _confguration = configuration;
            _nombreController = "ReservaController";
            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;

        }

        [HttpPost]
        [Route("CrearReserva")]
        public async Task<ActionResult> CrearReserva(ReservaDTO reserva)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }

                var result = await _reservaInterface.CrearReserva(reserva);
                if (result.Success)
                {
                    return Ok(new MessageInfoDTO().AccionCompletada(result.Message ?? string.Empty));
                }
                else
                {
                    return BadRequest(new MessageInfoDTO().AccionFallida(result.Message ??  string.Empty, (int)HttpStatusCode.BadRequest));
                }

            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear la reserva"));
            }
        }


        [HttpDelete]
        [Route("EliminarReserva")]
        public async Task<ActionResult> EliminarReserva(long IdReserva)
        {
            try
            {
                var result = await _reservaInterface.EliminarReserva(IdReserva);

                if (result.Success)
                {
                    return Ok(result.Success);
                }
                else
                {
                    return BadRequest(result.Message);
                }

            }catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar los tipos de incidencia"));

            }
        }

        [HttpPut]
        [Route("ActualizarReserva")]
        public async Task<ActionResult> ActualizarReserva(ReservaDTO reserva)
        {
            try
            {
                var result = await _reservaInterface.ActualizarReserva(reserva);

                if(result.Success)
                {
                    return Ok(result.Success);
                }
                else
                {
                    return BadRequest(result.Message);
                }


            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al actualizar las reservas"));
            }
        }

        [HttpGet]
        [Route("GetAllReservas")]
        public async Task<ActionResult> GetAllReservas()
        {
            try {
                var result = await _reservaInterface.ObtenerTodasLasReservas();

                return Ok(result);
            
            }catch(Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar las reservas"));
            }
        }



        

    }
}
