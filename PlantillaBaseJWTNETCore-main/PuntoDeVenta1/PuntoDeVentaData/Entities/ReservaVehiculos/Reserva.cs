using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.ReservaVehiculos
{
    public class Reserva : CrudEntities
    {
        [Key]
        public long IdReserva { get; set; }
        [ForeignKey("Vehiculo")]
        public long IdVehiculo { get; set; } 
        public DateTime FechaReserva { get; set; }
        [MaxLength(100)]
        public string ClienteNombre { get; set; } = "";
        [MaxLength(100)]
        public string ClienteTelefono { get; set; } = "";
        
        public virtual Vehiculo? Vehiculo { get; set; }

    }
}
