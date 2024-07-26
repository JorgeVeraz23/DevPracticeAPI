using Data.Dto.BibliotecaDTO;
using Data.Interfaces.BibliotecaInterfaces;
using System;
using System.Collections.Generic;
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

        public Task<List<AutorDTO>> GetAutoresLibros()
        {
            var autoresLibros = 
        }
    }
}
