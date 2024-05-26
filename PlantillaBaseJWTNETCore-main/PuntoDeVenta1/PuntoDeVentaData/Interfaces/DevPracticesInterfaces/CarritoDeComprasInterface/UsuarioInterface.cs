using Data.Dto.DevPracticesDTO.CarritoDeComprasDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.DevPracticesInterfaces.CarritoDeComprasInterface
{
    public interface UsuarioInterface
    {
        public Task<List<UsuarioDTO>> GetAll();
        public Task<UsuarioDTO> Get(long Id);
        public Task<MessageInfoDTO> Desactive(long Id);
        public Task<MessageInfoDTO> Create(UsuarioDTO data);
        public Task<MessageInfoDTO> Edit(UsuarioDTO data);
    }
}
