using Data;
using Data.Dto.ReservaVehiculoDTO;
using Data.Dto.TransaccionesEntreCuentasDTO;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Data.Interfaces.TransaccionEntreCuentaInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System.Net;

namespace PuntoDeVentaAPI.Controllers.TransaccionesEntreCuentaController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly CuentaInterface _cuentaInterface;
        private readonly ApplicationDbContext _context;
        private readonly string _nombreController;


        public CuentaController(ApplicationDbContext context, CuentaInterface cuentaInterface)
        {
            _cuentaInterface = cuentaInterface;
            _context = context;
            _nombreController = "CuentaController";
        }



        [HttpPost]
        public async Task<ActionResult> CrearCuenta(CuentaDTO cuenta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }

                var result = await _cuentaInterface.CrearCuenta(cuenta);
                if (result.Success)
                {
                    return Ok(new MessageInfoDTO().AccionCompletada(result.Message ?? string.Empty));
                }
                else
                {
                    return BadRequest(new MessageInfoDTO().AccionFallida(result.Message ?? string.Empty, (int)HttpStatusCode.BadRequest));
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear la reserva"));
            }
        }



    }
}
