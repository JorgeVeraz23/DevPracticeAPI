using Xunit;
using Moq;
using Data.Interfaces.XUnitInterface;
using Data;
using Data.Service;
using Moq;
using Xunit;
using Data.Dto.XUnitDTO;
using Data.Entities.UnitTest;

namespace Test
{
    public class LoanProcessingServiceTests
    {
        private readonly LoanProcessingService _service;
        private readonly Mock<ILoanApplicationRepository> _mockLoanRepo;
        private readonly ApplicationDbContext _context;
       

        public LoanProcessingServiceTests(ApplicationDbContext context)
        {
            // Configurar mocks
            _mockLoanRepo = new Mock<ILoanApplicationRepository>();
            _context = context;
            // Crear instancia del servicio con mocks
            _service = new LoanProcessingService(_mockLoanRepo.Object);
        }

        [Fact]
        public void ProcessLoanApplication_ValidApplication_Success()
        {
            //Arrange
            var applicationDTO = new LoanApplicationDTO { CustomerId = 1, Amount = 2000 };

            _mockLoanRepo.Setup(r => r.Add(It.IsAny<LoanApplication>()));

            //Act
            bool result = _service.ProcessLoanApplication(applicationDTO);

            //Asert
            Assert.True(result, "La solicitud de prestamo deberia ser aprobada" );
            _mockLoanRepo.Verify(r => r.Add(It.IsAny<LoanApplication>()), Times.Once);
           
        }



    }
}
