using Data.Dto.TransaccionesEntreCuentasDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.TransaccionEntreCuentaInterface
{
    public interface TransaccionInterface
    {

        public Task<MessageInfoDTO> CrearTransaccion();
        public Task<MessageInfoDTO> ActualizarTransaccion();
        public Task<MessageInfoDTO> EliminarCuenta();
        public Task<List<TransaccionDTO>> MostrarTodasLasTransacciones();
        public Task<List<TransaccionDTO>> MostrarTodasLasTransaccionesDeCuenta(long idCuenta);

    }
}
