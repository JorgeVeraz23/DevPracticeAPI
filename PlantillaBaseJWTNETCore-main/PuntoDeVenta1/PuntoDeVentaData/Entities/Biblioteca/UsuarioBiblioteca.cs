using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Biblioteca
{
    public class UsuarioBiblioteca : CrudEntities
    {
        [Key]
        public long IdUsuario { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        public virtual ICollection<Prestamos> Prestamos { get; set;} = new List<Prestamos>();
    }
}
