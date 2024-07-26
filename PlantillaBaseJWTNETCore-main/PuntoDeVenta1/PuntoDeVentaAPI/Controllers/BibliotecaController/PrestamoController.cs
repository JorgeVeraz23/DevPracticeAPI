using Data;
using Data.Interfaces.BibliotecaInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuntoDeVentaAPI.Controllers.BibliotecaController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly PrestamoInterface _prestamoInterface;
        private readonly ApplicationDbContext _context;



        public PrestamoController(PrestamoInterface prestamoInterface, ApplicationDbContext context)
        {
            _context = context;
            _prestamoInterface = prestamoInterface;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllPrestamo(long idUsuario)
        {
            try
            {
                var resultado = await _prestamoInterface.ObtenerPrestamosDelMesActual(idUsuario);
                return Ok(resultado);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
