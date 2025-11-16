using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.LogOpValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.LogOperation
{
    public static class LogOperationDependency
    {
        public static void AddLogOperationDependency(this IServiceCollection Services)
        {
            Services.AddScoped<ILogOperationsRepository, LogOperationsRepository>();
            Services.AddScoped<IValidatorBase<LogOperationsDto>, LogOperationValidator>();
            Services.AddTransient<ILogOperationsService, LogOperationsService>();
        }
    }
}
