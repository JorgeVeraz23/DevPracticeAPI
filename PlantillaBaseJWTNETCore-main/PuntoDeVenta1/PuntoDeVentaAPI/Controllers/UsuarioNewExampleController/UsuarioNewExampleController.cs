using Data.Dto.ExampleUseCallBackUseFetchDTO;
using Data.Interfaces.ExampleUseCallBackUseFetch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuntoDeVentaAPI.Controllers.UsuarioNewExampleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioNewExampleController : ControllerBase
    {
        private readonly UsuarioNewInterface _usuarioNewInterface;


        public UsuarioNewExampleController(UsuarioNewInterface usuarioNewInterface)
        {
            _usuarioNewInterface = usuarioNewInterface;            
        }


        [HttpGet("GetAllNewUsers")]
        public async Task<ActionResult> GetAllNewUsers()
        {
            var response = await _usuarioNewInterface.GetAll();

            return Ok(response);
        }

        [HttpGet("GetNewUsersById")]
        public async Task<ActionResult> GetNewUsersById(long id)
        {
            var response = await _usuarioNewInterface.GetById(id);

            return Ok(response);
        }


        [HttpDelete("DeleteNewUser")]
        public async Task<ActionResult> DeleteNewUser(long id)
        {
            var response = await _usuarioNewInterface.Delete(id);

            return Ok(response);
        }


        [HttpPost("CreateNewUser")]
        public async Task<ActionResult> CreateNewUser(UsuarioExampleDTO usuarioExampleDTO)
        {
            var response = await _usuarioNewInterface.Create(usuarioExampleDTO);


            return Ok(response);
        }

    }
}
