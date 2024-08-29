using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.MultiSelect
{
    public class UsuarioTag : CrudEntities
    {
        public long IdUsuarioMultiSelect { get; set; }
        public UsuarioMultiSelect? UsuarioMultiSelect { get; set; }


        public long IdTagMultiSelect { get; set; }
        public TagMultiSelect? TagMultiSelect { get; set; }

    }
}
