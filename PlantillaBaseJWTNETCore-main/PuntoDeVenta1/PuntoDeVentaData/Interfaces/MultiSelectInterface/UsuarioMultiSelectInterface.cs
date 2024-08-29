using Data.Dto.MultiSelectDTO;
using Data.Entities.MultiSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.MultiSelectInterface
{
    public interface UsuarioMultiSelectInterface
    {
        public Task<UsuarioMultiSelectDTO> GetUsuarioByIdAsync(long idUsuarioMultiSelect);
        public Task SaveSelectedTagsAsync(long idUsuarioMultiSelect, List<long> selectedTags);
        public Task<bool> CrearUsuarioMultiSelect(UsuarioMultiSelectDTO usuarioDto);

    }
}
