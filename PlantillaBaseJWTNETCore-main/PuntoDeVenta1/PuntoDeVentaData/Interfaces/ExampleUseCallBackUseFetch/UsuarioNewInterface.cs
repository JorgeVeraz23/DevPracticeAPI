using Data.Dto.DevPracticesDTO.CarritoDeComprasDTO;
using Data.Dto.ExampleUseCallBackUseFetchDTO;
using Data.Entities.ExampleUseCallBackUseFetch;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ExampleUseCallBackUseFetch
{
    public interface UsuarioNewInterface
    {
        public Task<List<UsuarioExampleDTO>> GetAll();
        public Task<UsuarioExampleDTO> GetById(long id);
        public Task<MessageInfoDTO> Update(UsuarioExampleDTO usuarioExampleDTO);
        public Task<MessageInfoDTO> Create(UsuarioExampleDTO usuarioExampleDTO);
        public Task<MessageInfoDTO> Delete(long id);

    }
}
