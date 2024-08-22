using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Dto.UtilitiesDTO.EnumeradoresDTO;

namespace Data.Dto.TransaccionesEntreCuentasDTO
{
    public class CuentaDTO
    {

        public long IdCuenta { get; set; }
        public long NumeroCuenta { get; set; }
        [MaxLength(100)]
        public string NombreTitular { get; set; } = "";
        [Required]
        public string CedulaTitular { get; set; } = 0;
        public EnumTipoCuenta TipoCuenta { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
