using Data.Dto.TransaccionesEntreCuentasDTO;
using Data.Entities.TransaccionesEntreCuentas;
using Data.Interfaces.SecurityInterfaces;
using Data.Interfaces.TransaccionEntreCuentaInterface;
using DocumentFormat.OpenXml.Drawing;
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

namespace Data.Repository.TransaccionesEntreCuentaRepository
{
    public class CuentaBancariaRepository : CuentaInterface
    {

        private readonly ApplicationDbContext _context;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly string _username;
        private readonly string _ip;

        public CuentaBancariaRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
        {

            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unitOfWorkRepository = unitOfWorkRepository;
            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () => (
                await userManager.FindByNameAsync(
                httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""
            )
                )?.UserName ?? "Desconocido").Result;

        }



        public Task<MessageInfoDTO> ActualizarCuenta()
        {
            throw new NotImplementedException();
        }

        public Task<MessageInfoDTO> ActualizarCuenta(CuentaDTO cuenta)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageInfoDTO> CrearCuenta(CuentaDTO cuenta)
        {
            try
            {
                var cuentaValidacion = await _context.Cuentas.Where(x => x.Active && x.CedulaTitular.ToUpper().Equals(cuenta.CedulaTitular.ToUpper())).FirstOrDefaultAsync() ;

                if (cuentaValidacion != null)
                {
                   
                    infoDTO.AccionFallida("Ya existe una cuenta para el usuario ingresado", 400);
                    return infoDTO;
                }

                if (cuenta.SaldoDisponible < 0)
                {
                    infoDTO.AccionFallida("No se puede crear una cuenta con saldo negativo", 400);
                    return infoDTO;
                }


                Cuenta cuentaEntidad = new Cuenta
                {
                    Active = true,
                    NumeroCuenta = cuenta.NumeroCuenta,
                    CedulaTitular = cuenta.CedulaTitular,
                    NombreTitular = cuenta.NombreTitular,
                    TipoCuenta = cuenta.TipoCuenta,
                    SaldoDisponible = cuenta.SaldoDisponible,
                    DateRegister = DateTime.Now,
                    UserRegister = _username,
                    IpRegister = _ip
                };

                await _context.Cuentas.AddAsync(cuentaEntidad);
                await _unitOfWorkRepository.SaveChangesAsync();

                infoDTO.AccionCompletada("Se ha creado la cuenta");
                return infoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }




        public async Task<MessageInfoDTO> EliminarCuenta(long idCuenta)
        {
            try { 

                var cuentaToDelete = await _context.Cuentas.FirstOrDefaultAsync(x => x.IdCuenta == idCuenta);

                if(cuentaToDelete == null)
                {
                    infoDTO.AccionFallida("No existe la cuenta a eliminar", 400);
                    return infoDTO;
                }


                cuentaToDelete.Active = false;
                cuentaToDelete.DateDelete = DateTime.Now;
                cuentaToDelete.IpDelete = _ip;
                cuentaToDelete.UserDelete = _username;

                await _unitOfWorkRepository.SaveChangesAsync();

                infoDTO.AccionCompletada("La cuenta se a eliminado exitosamente");
                return infoDTO;


            }catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<List<CuentaDTO>> MostrarCuentas()
        {
            try
            {
                var cuenta = await _context.Cuentas.Where(x => x.Active).Select(c => new CuentaDTO
                {
                    IdCuenta = c.IdCuenta,
                    NumeroCuenta = c.NumeroCuenta,
                    NombreTitular = c.NombreTitular,
                    CedulaTitular = c.CedulaTitular,
                    TipoCuenta = c.TipoCuenta,
                    SaldoDisponible = c.SaldoDisponible,
                }).ToListAsync();


                return cuenta;
            }catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public Task<CuentaDTO> ObtenerCuentaPorId(long idCuenta)
        {
            throw new NotImplementedException();
        }

        public Task<CuentaDTO> ObtenerCuentaPorNombre(string Nombre)
        {
            throw new NotImplementedException();
        }
    }
}
