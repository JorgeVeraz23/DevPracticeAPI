using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Entities.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Prueba
{
    [Table("BOOK", Schema = "LIB")]
    public class Book :CrudEntities
    {
        [Key]
        public long IdBook { get; set; }
        [MaxLength(100)]
        public string Codigo { get; set; } = "";
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        public int stock { get; set; }
        [Precision(6,2)]
        public decimal precio { get; set; }
    }
}
