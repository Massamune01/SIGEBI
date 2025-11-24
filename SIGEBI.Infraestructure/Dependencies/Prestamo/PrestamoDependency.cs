using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.PrestamosValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Prestamo
{
    public static class PrestamoDependency
    {
        public static void AddPrestamoDependency(this IServiceCollection Services)
        {
            Services.AddScoped<IPrestamoFacade, PrestamoFacade>();
            Services.AddScoped<IPrestamosRepository, PrestamosRepository>();
            Services.AddScoped<IValidatorBase<PrestamoDto>, PrestamoValidator>();
            Services.AddTransient<IPrestamosService, PrestamosServices>();
        }
    }
}
