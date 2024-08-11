using Data.Dto.BibliotecaDTO;
using Data.Entities.Biblioteca;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.BibliotecaInterfaces
{
    public interface LibroInterface
    {
        public Task<MessageInfoDTO> CrearLibro(CrearLibroDto data);

    }
}
