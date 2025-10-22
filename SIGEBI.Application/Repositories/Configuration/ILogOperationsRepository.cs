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
    public interface ILogOperationsRepository : IBaseRepository<LogOperations>
    {
        Task<List<LogOperations>> GetByEntity(string entity);
        Task<List<LogOperations>> GetByTypeOperation(string typeOperation);
        Task<List<LogOperations>> GetByStatus(int statusOp);
        Task<List<LogOperations>> GetLogOpById(int  logId);
    }
}
