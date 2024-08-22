using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.TransaccionesEntreCuentas
{
    public class HistorialTransacciones : CrudEntities
    {
        [Key]
        public long idHistorialTransacciones { get; set; }
        


        public virtual ICollection<Transacciones> Transacciones { get; set; } = new List<Transacciones>();
    }
}
