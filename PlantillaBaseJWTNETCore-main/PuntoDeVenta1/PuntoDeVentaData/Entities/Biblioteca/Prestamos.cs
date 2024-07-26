using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Biblioteca
{
    public class Prestamos : CrudEntities
    {
        [Key]
        public long IdPrestamo { get; set; }

        public DateTime FechaPrestamos { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public virtual Libro? Libro { get; set; }

        public virtual UsuarioBiblioteca? UsuarioBiblioteca { get; set; }
    }
}

