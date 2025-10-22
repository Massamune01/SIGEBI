
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.AdminValidators;
using SIGEBI.Application.Validators.Configuration.BibliotecarioValidators;
using SIGEBI.Application.Validators.Configuration.ClienteValidators;
using SIGEBI.Application.Validators.Configuration.CredencialesValidators;
using SIGEBI.Application.Validators.Configuration.LibroValidators;
using SIGEBI.Application.Validators.Configuration.LogOpValidators;
using SIGEBI.Application.Validators.Configuration.PrestamosValidators;
using SIGEBI.Application.Validators.Configuration.RolValidators;
using SIGEBI.Infraestructure.Data.Configuration;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;
using SIGEBI.Infraestructure.Dependencies.Admin;
using SIGEBI.Infraestructure.Dependencies.Bibliotecario;
using SIGEBI.Infraestructure.Dependencies.Cliente;
using SIGEBI.Infraestructure.Dependencies.Libro;
using SIGEBI.Infraestructure.Dependencies.LogOperation;
using SIGEBI.Infraestructure.Dependencies.Prestamo;
using SIGEBI.Infraestructure.Dependencies.Roles;
using SIGEBI.Infraestructure.Dependencies.Credenciales;

namespace SIGEBI.API1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Var Connection String
            var connectionString = builder.Configuration.GetConnectionString("SIGEBI_BD");
            //Adding HelperDb as Singleton
            builder.Services.AddSingleton(new HelperDb(connectionString));

            builder.Services.AddDbContext<SIGEBIContext>(options => options.UseSqlServer(connectionString));

            //Adding Services Dependencies
            // Add dependency injection for Ef repositories
            //Admin Dependency Injection
            builder.Services.AddAdminDependency();
            //Bibliotecario Dependency Injection
            builder.Services.AddBibliotecarioDependency();
            //Cliente Dependency Injection
            builder.Services.AddClienteDependency();
            //Libro Dependency Injection
            builder.Services.AddLibroDependency();
            //LogOperation Dependency Injection
            builder.Services.AddLogOperationDependency();
            //Prestamo Dependency Injection
            builder.Services.AddPrestamoDependency();
            //Rol Dependency Injection
            builder.Services.AddRolAdoDependency();
            //Credenciales Dependency Injection
            builder.Services.AddCredencialesDependency();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
