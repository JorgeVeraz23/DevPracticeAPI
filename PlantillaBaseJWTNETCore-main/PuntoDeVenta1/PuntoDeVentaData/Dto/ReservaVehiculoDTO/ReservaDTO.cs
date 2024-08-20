using Data.Entities.ReservaVehiculos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.ReservaVehiculoDTO
{
    public class ReservaDTO
    {

        public long IdReserva { get; set; }
        public long IdVehiculo { get; set; }
        public DateTime FechaReserva { get; set; }

        public string ClienteNombre { get; set; } = "";
        public string ClienteTelefono { get; set; } = "";

    }

    public class MostrarReservaDto
    {
        public long IdReserva { get; set; }
       
        public DateTime FechaReserva { get; set; }
        public string ClienteNombre { get; set; } = "";
        public string ClienteTelefono { get; set; } = "";
        public virtual Vehiculo? VehiculoVirtual { get; set; } 
    }
}
