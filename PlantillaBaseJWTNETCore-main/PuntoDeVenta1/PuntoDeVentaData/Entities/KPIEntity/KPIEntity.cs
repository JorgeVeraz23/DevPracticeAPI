using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.KPIEntity
{
    public class KPIEntity
    {
        [Key]
        public long IdKPI { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; }
    }
}
