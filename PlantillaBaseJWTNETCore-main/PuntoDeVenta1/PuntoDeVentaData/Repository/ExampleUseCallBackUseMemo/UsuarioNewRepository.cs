using Data.Dto.ExampleUseCallBackUseFetchDTO;
using Data.Entities.ExampleUseCallBackUseFetch;
using Data.Interfaces.ExampleUseCallBackUseFetch;
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

namespace Data.Repository.ExampleUseCallBackUseMemo
{
    public class UsuarioNewRepository : UsuarioNewInterface
    {
        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public UsuarioNewRepository(
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

        public async Task<List<UsuarioExampleDTO>> GetAll()
        {
            var listaNewUsuarios = await _context.Usuarioss.Select(c => new UsuarioExampleDTO
            {
                Id = c.IdUsuario,
                Nombre = c.Nombre,
                Edad = c.Edad,
            }).ToListAsync();


            return listaNewUsuarios;
        }

        public async Task<UsuarioExampleDTO> GetById(long id)
        {
            var usuarioExample = await _context.Usuarioss.Where(x => x.IdUsuario == id).Select(c => new UsuarioExampleDTO
            {
                Id = c.IdUsuario,
                Nombre = c.Nombre,  
                Edad = c.Edad,
            }).FirstOrDefaultAsync();


            return usuarioExample;

           
        }

        public async Task<MessageInfoDTO> Update(UsuarioExampleDTO usuarioExampleDTO)
        {
            //var usuario = await _context.Usuarioss.FirstOrDefaultAsync(x => x.IdUsuario == usuarioExampleDTO.Id);

            //if(usuario == null)
            //{
            //    infoDTO.Status = 404;
            //    infoDTO.Message = "No existe el usuario ingresado";
            //}
            //else
            //{

            //}

            throw new NotImplementedException();
        }

        public async Task<MessageInfoDTO> Create(UsuarioExampleDTO usuarioExampleDTO)
        {
            var infoDTO = new MessageInfoDTO(); // Asegúrate de inicializar infoDTO

            try
            {
                // Validación opcional (ejemplo: asegurarse de que los campos no están vacíos)
                if (string.IsNullOrWhiteSpace(usuarioExampleDTO.Nombre))
                {
                    infoDTO.Status = 400;
                    infoDTO.Message = "El nombre del usuario no puede estar vacío.";
                    return infoDTO;
                }

                if (usuarioExampleDTO.Edad <= 0)
                {
                    infoDTO.Status = 400;
                    infoDTO.Message = "La edad debe ser un valor positivo.";
                    return infoDTO;
                }

                // Mapeo del DTO al modelo de entidad
                Usuario usuarioNew = new Usuario
                {
                    Nombre = usuarioExampleDTO.Nombre,
                    Edad = usuarioExampleDTO.Edad
                };

                // Agregar y guardar la nueva entidad
                await _context.Usuarioss.AddAsync(usuarioNew);
                await _context.SaveChangesAsync();

                // Respuesta exitosa
                infoDTO.Status = 201;
                infoDTO.Message = "Usuario creado exitosamente";
                return infoDTO;
            }
            catch (Exception ex)
            {
                // Manejo de errores (log, mensaje de error genérico para no exponer detalles técnicos)
                infoDTO.Status = 500;
                infoDTO.Message = "Ocurrió un error al crear el usuario.";
                // Log el detalle del error (ex) si es necesario para depuración
                return infoDTO;
            }
        }


        public async Task<MessageInfoDTO> Delete(long id)
        {
            var infoDTO = new MessageInfoDTO(); // Asegúrate de inicializar infoDTO

            var usuario = await _context.Usuarioss.Where(x => x.IdUsuario == id).FirstOrDefaultAsync();

            if (usuario == null)
            {
                infoDTO.Status = 404;
                infoDTO.Message = "No existe el usuario ingresado";
                return infoDTO;
            }

            _context.Usuarioss.Remove(usuario); // Remover la entidad usuario, no solo el ID

            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            infoDTO.Status = 200;
            infoDTO.Message = "Usuario eliminado correctamente";
            return infoDTO;
        }
    }
}
