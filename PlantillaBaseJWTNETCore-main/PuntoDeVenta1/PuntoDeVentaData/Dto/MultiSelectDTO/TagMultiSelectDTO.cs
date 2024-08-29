using Data.Entities.MultiSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.MultiSelectDTO
{
    public class TagMultiSelectDTO
    {
        
        public string Nombre { get; set; }
    }

    public class SaveTagsRequesDTO
    {
        public long IdUsuario { get; set; }
        public List<long> SelectedTagsId { get; set; } = new List<long>();
    }
}
