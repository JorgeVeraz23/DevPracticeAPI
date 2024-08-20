using Data.Dto.ReservaVehiculoDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ReservaVehiculoInterfaces
{
    public interface VehiculoInterface
    {
        public Task<MessageInfoDTO> CrearVehiculo(VehiculoDTO vehiculo);
        public Task<MessageInfoDTO> ActualizarVehiculo(VehiculoDTO vehiculo);
        public Task<MessageInfoDTO> EliminarVehiculo(long idVehiculo);
        public Task<List<VehiculoDTO>> MostrarTodosLosVehiculos();
        public Task<VehiculoDTO> ObtenerVehiculoPorId(long idVehiculo);

    }
}
