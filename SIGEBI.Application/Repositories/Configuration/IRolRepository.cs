using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface IRolRepository : IBaseRepository<Roles>
    {
        Roles? GetRolById(int id);
        List<Roles> GetRolByName(string name);
    }
}
