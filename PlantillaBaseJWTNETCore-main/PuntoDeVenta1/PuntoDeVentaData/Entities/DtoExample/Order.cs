using Data.Entities.UnitTest;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.DtoExample
{
    public class Order : CrudEntities
    {
        [Key]
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("Cliente")]
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } // Relación con detalles de orden
    }
}

