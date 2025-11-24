using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface ILogOperationFacade
    {
        Task<ServiceResult> GetAllLogOperationsAsync();
        Task<ServiceResult> GetLogOperationsByIdAsync(int id);
        Task<ServiceResult> CreateLogOperationsAsync(CreateLogOperationDto logOpCreateDto);
        Task<ServiceResult> UpdateLogOperationsAsync(UpdateLogOperationDto logOpUpdateDto);
        Task<ServiceResult> DeleteLogOperationsAsync(int id);
    }
}
