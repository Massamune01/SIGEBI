
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.API1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Var Connection String
            var connectionString = builder.Configuration.GetConnectionString("SIGEBI_BD");
            builder.Services.AddSingleton(new SIGEBI.Infraestructure.Data.Configuration.HelperDb(connectionString));

            // Add services to the container.

            //Rol Dependency Injection
            builder.Services.AddScoped<IRolRepository, RolRepositoryAdo>();
                builder.Services.AddScoped<IRolService, RolService>();

                //Credenciales Dependency Injection
                builder.Services.AddScoped<ICredencialesRepository, CredencialesRepositoryAdo>();
                builder.Services.AddScoped<ICredencialesService, CredencialesService>();

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
