
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Profiles;
using SIGEBI.Domain.Interfaces.Cache;
using SIGEBI.Infraestructure.Cache;
using SIGEBI.Infraestructure.Data.Configuration;
using SIGEBI.Infraestructure.Dependencies.Admin;
using SIGEBI.Infraestructure.Dependencies.Bibliotecario;
using SIGEBI.Infraestructure.Dependencies.Cliente;
using SIGEBI.Infraestructure.Dependencies.Credenciales;
using SIGEBI.Infraestructure.Dependencies.Libro;
using SIGEBI.Infraestructure.Dependencies.LogOperation;
using SIGEBI.Infraestructure.Dependencies.Prestamo;
using SIGEBI.Infraestructure.Dependencies.Roles;
using SIGEBI.Persistence.Context;

namespace SIGEBI.API1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Var Connection String
            var connectionString = builder.Configuration.GetConnectionString("SIGEBI");
            //Adding HelperDb as Singleton
            builder.Services.AddSingleton(new HelperDb(connectionString));

            builder.Services.AddDbContext<SIGEBIContext>(options => options.UseSqlServer(connectionString));

            // Add Dependency injection for CacheService
            builder.Services.AddSingleton<ICacheService,  CacheLRUService>();

            builder.Services.AddAutoMapper(cfg => { }, typeof(AdminProfile));

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
