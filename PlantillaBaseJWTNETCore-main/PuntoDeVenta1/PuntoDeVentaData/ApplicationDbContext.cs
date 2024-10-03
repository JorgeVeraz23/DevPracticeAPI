using Data.Entities.Biblioteca;
using Data.Entities.Configurations;
using Data.Entities.DevPracticeEntities.CarritoDeComprasTutoEntities;
using Data.Entities.DtoExample;
using Data.Entities.ExampleUseCallBackUseFetch;
using Data.Entities.KPIEntity;
using Data.Entities.MultiSelect;
using Data.Entities.Prueba;
using Data.Entities.ReservaVehiculos;
using Data.Entities.TransaccionesEntreCuentas;
using Data.Entities.UnitTest;
using LinkQuality.Data.Repository.UtilitiesRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Entities.Parameters;
using PuntoDeVentaData.Entities.Security;
using PuntoDeVentaData.Entities.Templates;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }
        


        public DbSet<Order> Orders { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set;}
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<KPIEntity> KPIEntities { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<UsuarioBiblioteca> UsuarioBibliotecas { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Cuenta> Cuentas { get;set; }
        public DbSet<Transacciones> Transacciones { get; set; }

        public DbSet<UsuarioMultiSelect> UsuarioMultiSelects { get; set; }
        public DbSet<TagMultiSelect> TagMultiSelects { get; set; }
        public DbSet<UsuarioTag> UsuarioTags { get; set; }
        public DbSet<HistorialTransacciones> HistorialTransacciones { get; set; }
        public DbSet<Usuario> Usuarioss { get; set; }


        #region 
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        #endregion

        #region RESOURCE
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<Notificacion> Notificacions { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<BucketFile> BucketFiles { get; set; }

        #endregion

        #region SECURITY
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public virtual DbSet<AuditoryAccess> AuditoryAccesses { get; set; }
        public virtual DbSet<ApplicationVersion> ApplicationVersions { get; set; }
        #endregion

        #region SECURITY CONFIGS
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<IdentityUserRole<string>> UserRoles { get; set;}
        #endregion

        #region CLASIFICATION
        public virtual DbSet<ParameterType> ParameterTypes { get; set; }
        #endregion


        public virtual DbSet<Book> Book { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigurationDefaultRoles();
            modelBuilder.ConfigurationDefaultDataUsuario();
            modelBuilder.ConfigurationDefaultDataUserRol();
            modelBuilder.ConfigurationTablesUsersAndRols();
            modelBuilder.ConfigurationDefaultDataTipoParametro();
            modelBuilder.ConfigurationDefaultDataParametros();
            modelBuilder.ConfigurationDefaultDataMenu();

            modelBuilder.ConfigurationDefaultDataMenuRole();
            modelBuilder.ConfigurationDefaultDataEmailTemplate();


            //Configuracion de relacion muchos a muchos
            modelBuilder.Entity<UsuarioTag>()
                .HasKey(ut => new { ut.IdTagMultiSelect, ut.IdUsuarioMultiSelect });

            modelBuilder.Entity<UsuarioTag>()
                .HasOne(ut => ut.UsuarioMultiSelect)
                .WithMany(u => u.UsuarioTags)
                .HasForeignKey(ut => ut.IdUsuarioMultiSelect);

            modelBuilder.Entity<UsuarioTag>()
                .HasOne(ut => ut.TagMultiSelect)
                .WithMany(u => u.UsuarioTags)
                .HasForeignKey(ut => ut.IdTagMultiSelect);

            modelBuilder.Entity<Book>()
               .HasIndex(b => b.Codigo)
               .IsUnique();


        }

        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    // throw new ValidationException() or do whatever you want
                }
            }
            return base.SaveChanges();
        }
    }
}
