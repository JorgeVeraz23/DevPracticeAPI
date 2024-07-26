using Data.Entities.Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.BibliotecaDTO
{
    public class PrestamosDTO
    {
        public long IdPrestamo { get; set; }

        public DateTime FechaPrestamos { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public virtual ICollection<Libro>? Libro { get; set; }
        public virtual ICollection<UsuarioBiblioteca>? UsuarioBiblioteca { get; set; }
    }

    public class HistorialPrestamoDTO
    {
        public DateTime FechaPrestamo { get; set; }
        public string NombreLibro { get; set; }
        public string NombreUsuario { get; set; }
    }
}
