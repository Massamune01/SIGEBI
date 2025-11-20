using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Persistence.Context
{
    public class SIGEBIContext : DbContext
    {
        public SIGEBIContext(DbContextOptions<SIGEBIContext> options) : base(options)
        { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }

        #region "Entidades del módulo de Configuración"

        public DbSet<RolGetModel> RolGetModel { get; set; }
        public DbSet<RolCreateDto> RolCreateDto { get; set; }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<LibroDto> LibroDto { get; set; }
        public DbSet<LibroCreateDto> LibroCreateDto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteDto> ClienteDto { get; set; }
        public DbSet<Bibliotecarios> Bibliotecarios { get; set; }
        public DbSet<BibliotecarioDto> BibliotecariosDto { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<PrestamoDto> PrestamosDto { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminDto> AdminDto { get; set; }
        public DbSet<Credenciales> Credenciales { get; set; }
        public DbSet<CredencialesGetModel> CredencialesGetModel { get; set; }
        public DbSet<CredencialesCreateDto> CredencialesCreateDto { get; set; }
        public DbSet<LogOperations> LogOperations { get; set; }
        public DbSet<LogOperationsDto> LogOperationsDto { get; set; }

        #endregion
    }
}
