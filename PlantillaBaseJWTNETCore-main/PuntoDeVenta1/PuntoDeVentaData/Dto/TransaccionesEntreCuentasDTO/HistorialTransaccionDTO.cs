using Data.Entities.TransaccionesEntreCuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.TransaccionesEntreCuentasDTO
{
    public class HistorialTransaccionDTO
    {
        public long idHistorialTransacciones { get; set; }

        public List<Transacciones> Transacciones { get; set; } = new List<Transacciones>();
    }
}
