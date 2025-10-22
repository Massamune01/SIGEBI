using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface ICredencialesRepository : IBaseRepository<Credenciales>
    {
        Task<OperationResult> GetCredencialesById(int id);
        Task<OperationResult> GetCredencialesByClienteId(int clienteId);
        Task<OperationResult> GetCredencialesByUsuario(string usuario);
        Task<OperationResult> ClienteExist(int clienteId);
    }
}
