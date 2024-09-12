using AutoMapper;
using Data.Dto.DtoExampleDTO;
using Data.Interfaces.DtoExampleInterface;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Data.Repository.DtoExampleRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVentaData.Dto.UtilitiesDTO;

namespace PuntoDeVentaAPI.Controllers.DtoExampleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly OrderInterface _orderInterface;

        public OrderController(OrderInterface orderInterface, IMapper mapper)
        {
            _orderInterface = orderInterface;
            _mapper = mapper;
        }



       
        /// <summary>
        /// Endpoint de obtener ordenes por ID con DTO y AutoMapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerOrdenesPorId")]
        public async Task<ActionResult<OrderDto>> ObtenerOrdenesPorId(long id)
        {
            try
            {
                var order = await _orderInterface.GetOrder(id);

                if (order == null)
                {
                    return NotFound();
                }

                //Utilizamos AutoMapper para convertir la entidad de dominio a DTO
                var orderDto = _mapper.Map<OrderDto>(order);




                return Ok(orderDto);

            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, "OrderController", "Error al obtener el vehiculo ingresado"));
            }
        }

        /// <summary>
        /// Endpoint de obtener ordenes por ID sin DTO ni AutoMapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerOrdenesWithoutDto")]
        public async Task<ActionResult> ObtenerOrdenesWithoutDto(long id)
        {
            var order = await _orderInterface.GetOrder(id);

            if (order == null)
                return NotFound();

            return Ok(order); //Devolver la entidad Order directamente
        }



        /// <summary>
        /// Endpoint de obtener ordenes por id con DTO y sin AutoMapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerOrdenesWithoutMapperPorId")]
        public async Task<ActionResult<OrderDto>> ObtenerOrdenesWithoutMapperPorId(long id)
        {

            var orderDto = await _orderInterface.GetOrderWithoutMapper(id);

            if (orderDto == null)
                return NotFound();

            return Ok(orderDto);

        }

     
    }
}

