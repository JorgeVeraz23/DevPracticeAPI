using Data.Dto.BibliotecaDTO;
using Data.Entities.Biblioteca;
using Data.Interfaces.BibliotecaInterfaces;
using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Biblioteca
{
    public class LibroRepository : LibroInterface
    {
        private readonly ApplicationDbContext _context;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        public LibroRepository(ApplicationDbContext context)
        {
            _context = context;
 
        }

        public async Task<MessageInfoDTO> CrearLibro(CrearLibroDto data)
        {
            var autor = await _context.Autors.FindAsync(data.AutorId);

            // Normalizar el título a minúsculas para la comparación
            var tituloNormalizado = data.Titulo.ToLower();

            var ifAlreadyExist = await _context.Libros
                .Where(x => x.Active && x.Titulo.ToLower() == tituloNormalizado)
                .FirstOrDefaultAsync();

            if (ifAlreadyExist != null)
            {
                infoDTO.AccionFallida("Ya existe un libro registrado con ese titulo", 400);
                return infoDTO;
            }

            Libro libro = new Libro
            {
                Active = true,
                Titulo = data.Titulo,
                Autor = autor,
                AñoPublicacion = data.AñoPublicacion,
                DateRegister = DateTime.Now
            };

            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();

            infoDTO.AccionCompletada("Libro creado exitosamente");
            return infoDTO;


        }
    }
}
