using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.CredencialesValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Credenciales
{
    public static class CredencialesDependency
    {
        public static void AddCredencialesDependency(this IServiceCollection Services)
        {
            Services.AddScoped<ICredencialesRepository, CredencialesRepositoryAdo>();
            Services.AddScoped<IValidatorBase<CredencialesGetModel>, CredencialesValidator>();
            Services.AddTransient<ICredencialesService, CredencialesService>();
        }
    }
}
