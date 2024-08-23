using Data.Dto.TransaccionesEntreCuentasDTO;
using Data.Interfaces.TransaccionEntreCuentaInterface;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.TransaccionesEntreCuentaRepository
{
    public class TransaccionesRepository : TransaccionInterface
    {
        public Task<MessageInfoDTO> ActualizarTransaccion()
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> CrearTransaccion()
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> EliminarCuenta()
        {
            throw new NotImplementedException();
        }

        public Task<List<TransaccionDTO>> MostrarTodasLasTransacciones()
        {
            throw new NotImplementedException();
        }

        public Task<List<TransaccionDTO>> MostrarTodasLasTransaccionesDeCuenta(long idCuenta)
        {
            throw new NotImplementedException();
        }
    }
}
