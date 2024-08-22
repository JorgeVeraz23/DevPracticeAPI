using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Dto.UtilitiesDTO.EnumeradoresDTO;

namespace Data.Entities.TransaccionesEntreCuentas
{
    public class Transacciones : CrudEntities
    {
        [Key]
        public long IdTipoCuenta { get; set; }
        [ForeignKey("Cuenta")]
        public long IdCuenta { get; set; }
        [Required]

        public DateTime FechaTransaccion { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public decimal Saldo { get; set; }
        [Required]
        public EnumTipoTransaccion TipoTransaccion { get; set; }

        public virtual Cuenta Cuenta { get; set; } = new Cuenta();
    }
}
