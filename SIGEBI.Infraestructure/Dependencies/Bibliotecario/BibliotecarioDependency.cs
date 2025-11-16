using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.BibliotecarioValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Bibliotecario
{
    public static class BibliotecarioDependency
    {
        public static void AddBibliotecarioDependency(this IServiceCollection Services)
        {
            Services.AddScoped<IBibliotecariosRepository, BibliotecarioRepository>();
            Services.AddScoped<IValidatorBase<BibliotecarioDto>, BibliotecarioValidator>();
            Services.AddTransient<IBibliotecarioService, BibliotecarioService>();
        }
    }
}
