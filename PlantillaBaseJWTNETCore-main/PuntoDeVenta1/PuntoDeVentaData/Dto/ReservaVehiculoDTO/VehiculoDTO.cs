using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.ReservaVehiculoDTO
{
    public class VehiculoDTO
    {
        public long IdVehiculo { get; set; }

        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int Anio { get; set; }
        public bool Disponible { get; set; }
    }
}
