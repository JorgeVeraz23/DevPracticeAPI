using Data.Dto.XUnitDTO;
using Data.Entities.UnitTest;
using Data.Interfaces.XUnitInterface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public class LoanProcessingService
    {

        private readonly ILoanApplicationRepository _repository;
        private readonly ApplicationDbContext _context;

       

        public LoanProcessingService(ILoanApplicationRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }


        public LoanProcessingService(ILoanApplicationRepository repository)
        {
            _repository = repository;

        }
        public bool ProcessLoanApplication(LoanApplicationDTO applicationDTO)
        {
            //Convert DTO to Entity
            var application = new LoanApplication
            {
                CustomerId = applicationDTO.CustomerId,
                Amount = applicationDTO.Amount,
            };


            bool isValid = ValidateApplication(application);
            if(!isValid) return false;

            bool hasGoodCredit = CheckCredit(application.CustomerId, application.Amount);
            if(!hasGoodCredit) return false;

            bool isApproved = ApproveLoan(application);
            if (isApproved)
            {
                _repository.Add(application);
            }

            return isApproved;
        }

        private bool ValidateApplication(LoanApplication application)
        {
            var validarCustomer = _context.LoanApplications.Find(application.CustomerId);

            var customerValidado = false;
            if (validarCustomer != null)
            {
                customerValidado = true;
            }


            return application != null && customerValidado;
        }

        private bool CheckCredit(long customerId, decimal amount)
        {
            // Simulación: El crédito es bueno si el ID del cliente no es "badcredit"
            var customer = _context.Customers.Find(customerId);
            if(customer == null) {
                return false;
            }

            var aplica = false;

            if(customer.ScoreCrediticio <= 200 && amount <= 0)
            {
                return aplica;
            }

            if((customer.ScoreCrediticio >= 300 && customer.ScoreCrediticio <= 500) && amount > 0 && amount <= 2000)
            {
                aplica = true;
            }

            if((customer.ScoreCrediticio >= 600 && customer.ScoreCrediticio <= 900) && amount > 0 && amount <= 5000)
            {
                aplica = true;
            }
            

            
            return aplica;
        }

        private bool ApproveLoan(LoanApplication application)
        {
           var validarAplicacion = ValidateApplication(application);
           var validarCredito = CheckCredit(application.CustomerId, application.Amount);

            return validarAplicacion && validarCredito;
        }



    }
}
