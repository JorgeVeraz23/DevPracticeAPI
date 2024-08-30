using Data.Dto.MultiSelectDTO;
using Data.Interfaces.MultiSelectInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuntoDeVentaAPI.Controllers.UsuarioMultiSelectController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioMultiSelectController : ControllerBase
    {

        private readonly UsuarioMultiSelectInterface _usuarioMultiSelectInterface;

        public UsuarioMultiSelectController(UsuarioMultiSelectInterface usuarioMultiSelectInterface)
        {
            _usuarioMultiSelectInterface = usuarioMultiSelectInterface;
        }

        [HttpGet("ObtenerUsuarioPorId")]
        public async Task<ActionResult> ObtenerUsuarioPorId(long id)
        {
            try
            {
                var response = await _usuarioMultiSelectInterface.GetUsuarioByIdAsync(id);


                return Ok(response);


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AñadirTagsUsuario")]
        public async Task<ActionResult> AñadirTagsUsuario([FromBody] SaveTagsRequesDTO request)
        {
            if (request.SelectedTagsId.Count <= 0)
                return BadRequest("Debe seleccionar al menos un tag.");
            try
            {
               await _usuarioMultiSelectInterface.SaveSelectedTagsAsync(request.IdUsuario, request.SelectedTagsId);
                return Ok("Tags guardados correctamente");
            }catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar los tags: {ex.Message}");
            }
        }

        [HttpPost("CrearUsuarioMultiSelect")]
        public async Task<ActionResult> CrearUsuarioMultiSelect(UsuarioMultiSelectDTO usuarioMultiSelectDTO)
        {
            try
            {
                var response = await _usuarioMultiSelectInterface.CrearUsuarioMultiSelect(usuarioMultiSelectDTO);


                return Ok(response);


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
