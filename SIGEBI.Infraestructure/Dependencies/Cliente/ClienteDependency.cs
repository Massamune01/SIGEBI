using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.ClienteValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Cliente
{
    public static class ClienteDependency
    {
        public static void AddClienteDependency(this IServiceCollection Services)
        {
            Services.AddScoped<IClienteRepository, ClienteRepository>();
            Services.AddScoped<IValidatorBase<ClienteCreateDto>, ClienteCreateValidator>();
            Services.AddScoped<IValidatorBase<ClienteUpdateDto>, ClienteUpdateValidator>();
            Services.AddTransient<IClienteService, ClienteService>();
        }
    }
}
