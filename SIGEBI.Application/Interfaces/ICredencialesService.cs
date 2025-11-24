using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface ICredencialesService
    {
        Task<ServiceResult> GetCredencialesAllAsync();
        Task<ServiceResult> GetCredencialesByIdAsync(int id);
        Task<ServiceResult> CreateCredencialesAsync(CredencialesCreateDto createCredencialesDto);
        Task<ServiceResult> UpdateCredencialesAsync(CredencialesUpdateDto updateCredencialesDto);
        Task<ServiceResult> RemoveCredencialesAsync(CredencialesRemoveDto removeCredencialesDto);
    }
}
