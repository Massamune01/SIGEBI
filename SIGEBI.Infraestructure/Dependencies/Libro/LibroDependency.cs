using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.LibroValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Libro
{
    public static class LibroDependency
    {
        public static void AddLibroDependency(this IServiceCollection Services)
        {
            Services.AddScoped<ILibroFacade, LibroFacade>();
            Services.AddScoped<ILibrosRepository, LibroRepository>();
            Services.AddScoped<IValidatorBase<LibroDto>, LibroValidator>();
            Services.AddTransient<ILibroService, LibroService>();
        }
    }
}
