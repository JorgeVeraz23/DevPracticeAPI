﻿using Data.Interfaces.BibliotecaInterfaces;
using Data.Interfaces.CatalogsInterfaces;
using Data.Interfaces.DevPracticesInterfaces.CarritoDeComprasInterface;
using Data.Interfaces.DtoExampleInterface;
using Data.Interfaces.ExampleUseCallBackUseFetch;
using Data.Interfaces.IKPIRepository;
using Data.Interfaces.MultiSelectInterface;
using Data.Interfaces.ReservaVehiculoInterfaces;
using Data.Interfaces.SecurityInterfaces;
using Data.Interfaces.TemplateInterfaces;
using Data.Interfaces.TransaccionEntreCuentaInterface;
using Data.Interfaces.UserInterfaces;
using Data.Repository;
using Data.Repository.Biblioteca;
using Data.Repository.CatalogsRepository;
using Data.Repository.DevPracticeRepository.CarritoDeComprasRepository;
using Data.Repository.DtoExampleRepository;
using Data.Repository.ExampleUseCallBackUseMemo;
using Data.Repository.KPIRepository;
using Data.Repository.MultiSelectRepository;
using Data.Repository.ReservaVehiculoRepository;
using Data.Repository.TemplateRepository;
using Data.Repository.TransaccionesEntreCuentaRepository;
using Data.Repository.UtilitiesRepository;

using LinkQuality.Data.Repository.SecurityRepository;
using LinkQuality.Data.Repository.SeguridadRepository;
using LinkQuality.Data.Repository.UserRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DependencyContainer
    {
        public static IServiceCollection DependencyEF(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<OrderInterface, OrderRepository>();
            services.AddScoped<IKPIRepository, KPIRepository>();
            services.AddScoped<PrestamoInterface, PrestamoRepository>();
            services.AddScoped<AutorInterface, AutorRepository>();
            services.AddScoped<LibroInterface,LibroRepository>();
            services.AddScoped<UsuarioNewInterface, UsuarioNewRepository>();

            services.AddScoped<ReservaInterface, ReservaRepository>();
            services.AddScoped<VehiculoInterface, VehiculoRepository>();

            services.AddScoped<CuentaInterface, CuentaBancariaRepository>();
            services.AddScoped<TransaccionesRepository, TransaccionesRepository>();
            services.AddScoped<HistorialTransaccionesInterface, HistorialTransaccionesRepository>();

            services.AddScoped<ProductosInterface, ProductoRepository>();
            services.AddScoped<UsuarioInterface, UsuarioRepository>();

            services.AddScoped<IdentityDbContext<ApplicationUser, ApplicationRole, string>, ApplicationDbContext>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuRolRepository, MenuRoleRepository>();
            
            services.AddScoped<IAuditoriaAccesosRepository, AuditoriaAccesosRepository>();
            services.AddScoped<IRolRepository, RolRepository>();

            services.AddScoped<IEmailTemplate, TemplateRepository>();

            services.AddScoped<UserInterface, UserRepository>();
            services.AddScoped<GeneralCatalogsInterface, GeneralCatalogsRepository>();

            services.AddScoped<UsuarioMultiSelectInterface, UsuarioMultiSelectRepository>();
            services.AddScoped<TagMultiSelectInterface, TagMultiSelectRepository>();

            return services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}
