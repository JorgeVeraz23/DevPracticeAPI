using Data.Entities.KPIEntity;
using Data.Interfaces.IKPIRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.KPIRepository
{
    public class KPIRepository : IKPIRepository
    {

        private readonly ApplicationDbContext _context;

        public KPIRepository(ApplicationDbContext context)
        {

            _context = context;

        }

        public async Task CargaMasivaAsync(IEnumerable<KPIEntity> kpis)
        {
            await _context.KPIEntities.AddRangeAsync(kpis);
            await _context.SaveChangesAsync();
        }
    }
}
