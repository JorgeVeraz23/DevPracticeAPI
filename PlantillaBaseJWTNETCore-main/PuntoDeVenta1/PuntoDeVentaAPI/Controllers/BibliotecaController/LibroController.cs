using Data.Dto.BibliotecaDTO;
using Data.Interfaces.BibliotecaInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuntoDeVentaAPI.Controllers.BibliotecaController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {

        private readonly LibroInterface _libroInterface;

        public LibroController(LibroInterface libroInterface)
        {
            _libroInterface = libroInterface;
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> CrearLibro([FromBody] CrearLibroDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _libroInterface.CrearLibro(data);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
