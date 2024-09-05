using Data.Entities.KPIEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IKPIRepository
{
    public interface IKPIRepository
    {
        public Task CargaMasivaAsync(IEnumerable<KPIEntity> kpis);
        public Task<IEnumerable<KPIEntity>> obtenerKPIAsync(); //Metodo para obtener los KPIs

    }
}
