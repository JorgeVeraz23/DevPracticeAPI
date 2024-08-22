using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Dto.UtilitiesDTO.EnumeradoresDTO;

namespace Data.Entities.TransaccionesEntreCuentas
{
    public class Cuenta : CrudEntities
    {
        [Key]
        public long IdCuenta { get; set; }
        [Required]
        public long NumeroCuenta { get; set;  }
        [Required]
        [MaxLength(100)]
        public string NombreTitular { get; set; } = "";
        [Required]
        public string CedulaTitular { get; set; } = "";
        [Required]
        public EnumTipoCuenta TipoCuenta { get; set; }
        [Required]
        public decimal SaldoDisponible { get; set; }
    }
}
