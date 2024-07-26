using Data.Dto.BibliotecaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.BibliotecaInterfaces
{
    public interface PrestamoInterface
    {
        public Task<List<HistorialPrestamoDTO>> ObtenerPrestamosDelMesActual(long idUsuario);
    }
}
