using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface ICredencialesRepository : IBaseRepository<Credenciales>
    {
        Task<Credenciales?> GetCredencialesByIdAsync(int id);
        Task<Credenciales?> GetCredencialesByClienteIdAsync(int clienteId);
    }
}
