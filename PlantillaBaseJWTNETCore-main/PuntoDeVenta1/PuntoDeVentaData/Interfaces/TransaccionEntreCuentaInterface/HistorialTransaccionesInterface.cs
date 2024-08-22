using Data.Entities.TransaccionesEntreCuentas;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.TransaccionEntreCuentaInterface
{
    public interface HistorialTransaccionesInterface
    {

        public Task<MessageInfoDTO> CrearHistorialTransacciones();
        public Task<MessageInfoDTO> ActualizarHistorialTransacciones();
        public Task<MessageInfoDTO> EliminarHistorialDeTransacciones(long id);
        public Task<List<HistorialTransacciones>> MostrarTodosLosHistorialTransacciones();
        public Task<List<HistorialTransacciones>> ObtenerHistorialTransaccionesPorId(long id);
        public Task<List<HistorialTransacciones>> ObtenerHistorialTranssaccionesPorIdCuenta(long id);
    }
}
