using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.UtilitiesDTO
{
    public class EnumeradoresDTO
    {
        public enum EnumTipoCuenta
        {
            Ahorro = 1,
            Corriente = 2
        }

        public enum EnumTipoTransaccion
        {
            Retiro = 1,
            Transferencia = 2,
            Monto = 3
        }
    }
}
