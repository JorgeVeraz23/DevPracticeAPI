using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.BibliotecaDTO
{
    public class AutorDTO
    {

        public long IdAutor { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        [MaxLength(25)]
        public string Nacionalidad { get; set; } = "";
        public virtual ICollection<LibroDTO>? Libros { get; set; }
    }

    public class AutoresCantidadLibrosDTO
    {
        public long IdAutor { get; set; }
        public string Nombre { get; set; }
        public int CandidadLibros { get; set; }
        public virtual ICollection<LibroDTO>? Libros { get; set; } 

    }
}
