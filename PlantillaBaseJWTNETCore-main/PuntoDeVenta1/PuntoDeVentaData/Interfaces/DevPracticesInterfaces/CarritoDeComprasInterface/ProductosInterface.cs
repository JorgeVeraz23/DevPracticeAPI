using Data.Dto.DevPracticesDTO.CarritoDeComprasDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.DevPracticesInterfaces.CarritoDeComprasInterface
{
    public interface ProductosInterface
    {
        public Task<List<ProductoDTO>> GetAll();
        public Task<ProductoDTO> Get(long Id);
        public Task<MessageInfoDTO> Desactive(long Id);
        public Task<MessageInfoDTO> Create(ProductoDTO data);
        public Task<MessageInfoDTO> Edit(ProductoDTO data);
    }
}
