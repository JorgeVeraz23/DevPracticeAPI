using Data;
using Data.Interfaces.BibliotecaInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuntoDeVentaAPI.Controllers.BibliotecaController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {

        private readonly AutorInterface _autorInterface;
        private readonly ApplicationDbContext _context;

        public AutorController(AutorInterface autorInterface, ApplicationDbContext context)
        {
            _autorInterface = autorInterface;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerLibrosAutores()
        {
            try
            {
                var respuesta = await _autorInterface.GetAutoresLibros();

                return Ok(respuesta);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
