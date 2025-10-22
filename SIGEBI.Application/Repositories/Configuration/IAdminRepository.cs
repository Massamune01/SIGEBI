using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<List<Admin>> GetAdminByIdAsync(int id);
        Task<List<Admin>> GetAdminByEmailAsync(string email);
        Task<List<Admin>> GetAdminByNameAsync(string name);
        Task<List<Admin>> GetAdminByCedulaAsync(string cedula);
        Task<List<Admin>> GetAdminByRolAsync(int rol);
        Task<List<Admin>> GetAdminByStatusAsync(Status status);
    }
}
