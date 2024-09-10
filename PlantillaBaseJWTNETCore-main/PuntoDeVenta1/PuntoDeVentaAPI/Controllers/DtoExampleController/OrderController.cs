using Data.Interfaces.DtoExampleInterface;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVentaData.Dto.UtilitiesDTO;

namespace PuntoDeVentaAPI.Controllers.DtoExampleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly OrderInterface _orderInterface;

        public OrderController(OrderInterface orderInterface)
        {
            _orderInterface = orderInterface;
        }

        [HttpGet]
        [Route("ObtenerOrdenesPorId")]
        public async Task<ActionResult> ObtenerOrdenesPorId(long id)
        {
            try
            {
                var result = await _orderInterface.GetOrder(id);


                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, "OrderController", "Error al obtener el vehiculo ingresado"));
            }
        }
    }
}
