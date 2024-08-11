using Data.Entities.Biblioteca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.BibliotecaDTO
{
    public class LibroDTO
    {

        public long IdLibro { get; set; }
        [MaxLength(100)]
        public string Titulo { get; set; } = "";
        [ForeignKey("Autor")]
        public long AutorId { get; set; }
        public DateTime AñoPublicacion { get; set; }
        public virtual Prestamos Prestamos { get; set; }
        public virtual Autor? Autor { get; set; }
    }

    public class CrearLibroDto
    {
        public long IdLibro { get; set; }
        public string Titulo { get; set; }
        public DateTime AñoPublicacion { get; set; }
        public long AutorId { get; set; }

        public class CantidadLibrosDeAutor
        {
            public long IdLibro { get; set; }
            public string Titulo { get; set; }
            public int CantidadLibros { get; set; }
            public virtual ICollection<AutorDTO> Autors { get; set; }
        }
    }
}