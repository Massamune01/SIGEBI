using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Persistence.Context
{
    public class SIGEBIContext : DbContext
    {
        public SIGEBIContext(DbContextOptions<SIGEBIContext> options) : base(options)
        { }

        #region "Entidades del módulo de Configuración"

        public DbSet<Libro> Libro { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Bibliotecarios> Bibliotecarios { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminDto> AdminDto { get; set; }
        public DbSet<Credenciales> Credenciales { get; set; }
        public DbSet<LogOperations> LogOperations { get; set; }

        #endregion
    }
}
