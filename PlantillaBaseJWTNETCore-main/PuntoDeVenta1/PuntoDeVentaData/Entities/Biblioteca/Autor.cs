using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Biblioteca
{
    public class Autor : CrudEntities
    {
        [Key]
        public long IdAutor { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        [MaxLength(25)]
        public string Nacionalidad { get; set; } = "";
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
