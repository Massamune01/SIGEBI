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
        List<Admin> GetAdminById(int id);
        List<Admin> GetAdminByName(string name);
        List<Admin> GetAdminByRol(int rol);
        List<Admin> GetAdminByStatus(Status status);
    }
}
