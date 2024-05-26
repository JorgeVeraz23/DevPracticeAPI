using Data.Dto.DevPracticesDTO.CarritoDeComprasDTO;
using Data.Interfaces.DevPracticesInterfaces.CarritoDeComprasInterface;
using Data.Interfaces.SecurityInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.DevPracticeRepository.CarritoDeComprasRepository
{
    public class UsuarioRepository : UsuarioInterface
    {
        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;

        public UsuarioRepository(
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


        public Task<MessageInfoDTO> Create(UsuarioDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> Desactive(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> Edit(UsuarioDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDTO> Get(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsuarioDTO>> GetAll()
        {
            var usuarios = await _context.Usuarios.Where(x => x.Active).Select(x => new UsuarioDTO
            {
                IdUsuario = x.IdUsuario,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Email = x.Email,
                Contraseña = x.Contraseña
            }).ToListAsync();
            return usuarios;
        }
    }
}
