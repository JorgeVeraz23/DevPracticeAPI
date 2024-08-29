using Data.Dto.MultiSelectDTO;
using Data.Interfaces.MultiSelectInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuntoDeVentaAPI.Controllers.UsuarioMultiSelectController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagMultiSelectController : ControllerBase
    {

        private readonly TagMultiSelectInterface _tagMultiSelectInterface;

        public TagMultiSelectController(TagMultiSelectInterface tagMultiSelectInterface)
        {
            _tagMultiSelectInterface = tagMultiSelectInterface;
        }


        [HttpPost]
        public async Task<IActionResult> CrearTag([FromBody]TagMultiSelectDTO tagMultiSelectDTO)
        {
            try
            {
                var response = await _tagMultiSelectInterface.CrearTagMuliSelect(tagMultiSelectDTO);

                return Ok(response);


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
