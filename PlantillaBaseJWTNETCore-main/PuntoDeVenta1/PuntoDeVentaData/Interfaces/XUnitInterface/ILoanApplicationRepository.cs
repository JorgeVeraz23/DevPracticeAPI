using Data.Dto.XUnitDTO;
using Data.Entities.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.XUnitInterface
{
    public interface ILoanApplicationRepository
    {
        LoanApplication GetById(long id);
        void Add(LoanApplication application);
        void Update(LoanApplication application);   
        bool Exists(long id);

    }
}
