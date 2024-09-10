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
    public class OrderDetail : CrudEntities
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
