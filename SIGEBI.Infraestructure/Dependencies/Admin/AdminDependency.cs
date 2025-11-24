using Microsoft.Extensions.DependencyInjection;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Application.Validators.Configuration.AdminValidators;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Infraestructure.Dependencies.Admin
{
    public static class AdminDependency
    {
        public static void AddAdminDependency(this IServiceCollection services)
        {
            services.AddScoped<IAdminFacade, AdminFacade>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IValidatorBase<AdminDto>, AdminValidator>();
            services.AddTransient<IAdminService, AdminServices>();
        }
    }
}
