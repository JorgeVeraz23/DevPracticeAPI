using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Biblioteca
{
    public class Libro : CrudEntities
    {
        [Key]
        public long IdLibro { get; set; }
        [MaxLength(100)]
        public string Titulo { get; set; } = "";
        public DateTime AñoPublicacion { get; set; }
        public virtual ICollection<Prestamos>? Prestamos { get; set; }
        public virtual Autor? Autor { get; set; }
    }
}
