using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.RolValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Roles
{
    public static class RolAdoDependency
    {
        public static void AddRolAdoDependency(this IServiceCollection Services)
        {
            Services.AddScoped<IRolesFacade, RolesFacade>();
            Services.AddScoped<IRolRepository, RolRepositoryAdo>();
            Services.AddScoped<IValidatorBase<RolDto>, RolValidator>();
            Services.AddTransient<IRolService, RolService>();
        }
    }
}
