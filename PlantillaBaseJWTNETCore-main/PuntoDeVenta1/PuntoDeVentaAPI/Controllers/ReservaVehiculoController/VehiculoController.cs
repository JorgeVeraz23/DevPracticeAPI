using AutoMapper;
using Data;
using Data.Dto.ReservaVehiculoDTO;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System.Net;

namespace PuntoDeVentaAPI.Controllers.ReservaVehiculoController
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly VehiculoInterface _vehiculoInterface;
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




        public VehiculoController(VehiculoInterface vehiculoInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider serviceProvider, IMapper mapper, IConfiguration configuration)
        {
            _vehiculoInterface = vehiculoInterface;
            this._context = applicationDbContext;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _confguration = configuration;
            _nombreController = "VehiculoController";
            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;

        }

        [HttpGet]
        [Route("GetAllVehiculos")]
        public async Task<ActionResult> GetAllVehiculos()
        {
            try
            {
                var result = await _vehiculoInterface.MostrarTodosLosVehiculos();

                return Ok(result);

            }catch(Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los vehiculos"));
            }
        }

        [HttpGet]
        [Route("ObtenerVehiculoPorId")]
        public async Task<ActionResult> ObtenerVehiculoPorId(long id)
        {
            try
            {
                var result = await _vehiculoInterface.ObtenerVehiculoPorId(id);


                return Ok(result);

            }catch(Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al obtener el vehiculo ingresado"));
            }
        }

        [HttpPost]
        [Route("CrearVehiculo")]
        public async Task<ActionResult> CrearVehiculo(VehiculoDTO vehiculo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }

                var resultSave = await _vehiculoInterface.CrearVehiculo(vehiculo);
                if (resultSave.Success)
                {
                    return Ok(new MessageInfoDTO().AccionCompletada(resultSave.Message ?? string .Empty));
                }
                else
                {
                    return BadRequest(new MessageInfoDTO().AccionFallida(resultSave.Message ?? string.Empty, (int)HttpStatusCode.BadRequest));
                }




            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al crear los vehiculos"));
            }
        }

        [HttpPut]
        [Route("ActualizarVehiculo")]
        public async Task<ActionResult> ActualizarVehiculo(VehiculoDTO vehiculo)
        {
            try
            {
                var result = await _vehiculoInterface.ActualizarVehiculo(vehiculo);

                if (result.Success)
                {
                    return Ok(result.Success);
                }
                else
                {
                    return BadRequest(result.Message ?? string.Empty);
                }

            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al actualizar los vehiculos"));
            }
        }

        [HttpDelete]
        [Route("EliminarVehiculo")]
        public async Task<ActionResult> EliminarVehiculo(long idVehiculo)
        {
            try
            {
                var result = await _vehiculoInterface.EliminarVehiculo(idVehiculo);

                if (result.Success)
                {
                    return Ok(result.Success);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }catch(Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar el vehiculo"));
            }

        }


    }
}
