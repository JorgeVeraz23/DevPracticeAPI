using Data.Entities.TransaccionesEntreCuentas;
using Data.Interfaces.TransaccionEntreCuentaInterface;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.TransaccionesEntreCuentaRepository
{
    public class HistorialTransaccionesRepository : HistorialTransaccionesInterface
    {
        public Task<MessageInfoDTO> ActualizarHistorialTransacciones()
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> CrearHistorialTransacciones()
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> EliminarHistorialDeTransacciones(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HistorialTransacciones>> MostrarTodosLosHistorialTransacciones()
        {
            throw new NotImplementedException();
        }

        public Task<List<HistorialTransacciones>> ObtenerHistorialTransaccionesPorId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HistorialTransacciones>> ObtenerHistorialTranssaccionesPorIdCuenta(long id)
        {
            throw new NotImplementedException();
        }
    }
}
