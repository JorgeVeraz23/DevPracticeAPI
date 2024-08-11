using Data.Dto.BibliotecaDTO;
using Data.Entities.Biblioteca;
using Data.Interfaces.BibliotecaInterfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Biblioteca
{
    public class AutorRepository : AutorInterface
    {
        private readonly ApplicationDbContext _context;
        
        public AutorRepository(ApplicationDbContext context)
        {
            _context = context;
        }


      
        public async Task<List<AutoresCantidadLibrosDTO>> GetAutoresLibros()
        {

            var autoresLibrosCantidad = await _context.Autors.Include(x => x.Libros).Where(x => x.Active).Select(c => new AutoresCantidadLibrosDTO
            {
                IdAutor = c.IdAutor,
                Nombre = c.Nombre,
                Libros = c.Libros.Where(x => x.Active).Select(c => new LibroDTO
                {
                    Titulo = c.Titulo,
                    AñoPublicacion = c.AñoPublicacion,
                    IdLibro = c.IdLibro,
                    AutorId = c.Autor!.IdAutor,
                }).ToList(),
                CandidadLibros = c.Libros.Where(x => x.Active).Count(),


            }).ToListAsync();

           

            return autoresLibrosCantidad;
        }

        
    }
}
