using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        List<Cliente> GetClienteById(int id);
        List<Cliente> GetClienteByName(string name);
    }
}
