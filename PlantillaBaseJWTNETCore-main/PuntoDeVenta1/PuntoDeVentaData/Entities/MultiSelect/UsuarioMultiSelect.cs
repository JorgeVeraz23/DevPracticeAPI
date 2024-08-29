using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.MultiSelect
{
    public class UsuarioMultiSelect : CrudEntities
    {
        [Key]
        public long IdUsuarioTag { get; set; }  
        public string Nombre { get; set; }

        public virtual ICollection<UsuarioTag>? UsuarioTags { get; set; }
    }
}
