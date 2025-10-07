using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface ICredencialesService
    {
        Task<ServiceResult> GetCredencialesAll();
        Task<ServiceResult> GetCredencialesById(int id);
        Task<ServiceResult> CreateCredenciales(CredencialesCreateDto createCredencialesDto);
        Task<ServiceResult> UpdateCredenciales(CredencialesUpdateDto updateCredencialesDto);
        Task<ServiceResult> RemoveCredenciales(CredencialesRemoveDto removeCredencialesDto);
    }
}
