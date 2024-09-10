using Data.Entities.UnitTest;
using Data.Interfaces.XUnitInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.XUnitRepository
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {

        private readonly ApplicationDbContext _context;

        public LoanApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(LoanApplication application)
        {
            _context.LoanApplications.Add(application);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.LoanApplications.Any(a => a.Id == id);
        }

        public LoanApplication GetById(long id)
        {
            var loanApplication = _context.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                throw new KeyNotFoundException($"Loan application with ID {id} not found.");
            }
            return loanApplication;
        }

        public void Update(LoanApplication application)
        {
            _context.LoanApplications.Update(application);
            _context.SaveChanges();
        }
    }
}
