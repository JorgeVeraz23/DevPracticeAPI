using Data.Dto.BibliotecaDTO;
using Data.Interfaces.BibliotecaInterfaces;
using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PrestamoRepository : PrestamoInterface
    {
        ApplicationDbContext _context;
        public PrestamoRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<List<HistorialPrestamoDTO>> ObtenerPrestamosDelMesActual(long idUsuario)
        {
            var FechaActual = DateTime.Now;

            var verHistorialPrestamo = await _context.Prestamos.Where(x => x.Active && x.FechaPrestamos.Year == FechaActual.Year && x.FechaPrestamos.Date.Month == FechaActual.Month && x.UsuarioBiblioteca!.IdUsuario == idUsuario).Select(c => new HistorialPrestamoDTO
            {
                FechaPrestamo = c.FechaPrestamos,
                NombreLibro = c.Libro!.Titulo,
                NombreUsuario = c.UsuarioBiblioteca!.Nombre
            }).ToListAsync();

            return verHistorialPrestamo;
        }
    }
}
