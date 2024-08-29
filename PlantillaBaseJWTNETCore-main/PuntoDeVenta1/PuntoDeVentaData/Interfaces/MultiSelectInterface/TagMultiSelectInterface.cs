using Data.Dto.MultiSelectDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.MultiSelectInterface
{
    public interface TagMultiSelectInterface
    {
        public Task<bool> CrearTagMuliSelect(TagMultiSelectDTO tagMultiSelectDTO);
    }
}
