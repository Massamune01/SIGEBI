using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface ICredencialesFacade
    {
        Task<ServiceResult> CreateCredencialesAsync(CredencialesCreateDto credencialesCreateDto);
        Task<ServiceResult> GetCredencialesAllAsync();
        Task<ServiceResult> GetCredencialesByIdAsync(int id);
        Task<ServiceResult> RemoveCredencialesAsync(CredencialesRemoveDto credencialesRemoveDto);
        Task<ServiceResult> UpdateCredencialesAsync(CredencialesUpdateDto credencialesUpdateDto);
    }
}
