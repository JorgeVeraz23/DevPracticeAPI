using Data.Entities.TransaccionesEntreCuentas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Dto.UtilitiesDTO.EnumeradoresDTO;

namespace Data.Dto.TransaccionesEntreCuentasDTO
{
    public class TransaccionDTO
    {

        public long IdTipoCuenta { get; set; }
        public long IdCuenta { get; set; }

        public DateTime FechaTransaccion { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public EnumTipoTransaccion TipoTransaccion { get; set; }

        public virtual Cuenta Cuenta { get; set; } = new Cuenta();
    }
}
