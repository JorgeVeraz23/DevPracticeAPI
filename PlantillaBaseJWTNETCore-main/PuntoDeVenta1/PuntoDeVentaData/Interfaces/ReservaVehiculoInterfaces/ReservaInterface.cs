using Data.Dto.ReservaVehiculoDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ReservaVehiculoInterfaces
{
    public interface ReservaInterface
    {
        public Task<MessageInfoDTO> CrearReserva(ReservaDTO reserva);
        public Task<MessageInfoDTO> ActualizarReserva(ReservaDTO reserva);
        public Task<MessageInfoDTO> EliminarReserva (long idReserva);
        public Task<ReservaDTO> ObtenerReservaPorId(long idReserva);
        public Task<List<MostrarReservaDto>> ObtenerTodasLasReservas();
        public Task<MessageInfoDTO> EstablecerVehiculoNoDisponible(long idVehiculo);

    }
}
