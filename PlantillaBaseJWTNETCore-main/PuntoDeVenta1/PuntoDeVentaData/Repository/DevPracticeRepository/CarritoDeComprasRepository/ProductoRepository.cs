
using Data.Dto.DevPracticesDTO.CarritoDeComprasDTO;
using Data.Interfaces.DevPracticesInterfaces.CarritoDeComprasInterface;
using Data.Interfaces.SecurityInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;

namespace Data.Repository.DevPracticeRepository.CarritoDeComprasRepository
{
    public class ProductoRepository : ProductosInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public ProductoRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;

            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () =>
            (
                await userManager.FindByNameAsync(
                    httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""
                )
            )?.UserName ?? "Desconocido").Result;
        }

        public Task<MessageInfoDTO> Create(ProductoDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> Desactive(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> Edit(ProductoDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<ProductoDTO> Get(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductoDTO>> GetAll()
        {
            var productos = await _context.Productos.Where(x => x.Active).Select(c => new ProductoDTO
            {
                IdProductos = c.IdProductos,
                Nombre = c.Nombre,
                Precio = c.Precio,
                Categoria = c.Categoria,
            }).ToListAsync();
            return productos;
        }
    }
}
