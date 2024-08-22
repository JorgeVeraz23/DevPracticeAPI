using Data.Dto.TransaccionesEntreCuentasDTO;
using Data.Entities.TransaccionesEntreCuentas;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.TransaccionEntreCuentaInterface
{
    public interface CuentaInterface
    {
        public Task<MessageInfoDTO> CrearCuenta(CuentaDTO cuenta);
        public Task<MessageInfoDTO> ActualizarCuenta(CuentaDTO cuenta);
        public Task<MessageInfoDTO> EliminarCuenta(long idCuenta);
        public Task<List<CuentaDTO>> MostrarCuentas();
        public Task<CuentaDTO> ObtenerCuentaPorId(long idCuenta);
        public Task<CuentaDTO> ObtenerCuentaPorNombre(string Nombre);
    }
}
