using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.TransaccionesEntreCuentas
{
    public class SecuenciaNumeroCuenta : CrudEntities
    {
        [Key]
        public string NombreSecuencia { get; set; }
        public long UltimoNumero { get; set; }
    }
}
