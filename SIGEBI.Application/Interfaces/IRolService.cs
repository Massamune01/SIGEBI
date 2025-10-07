using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface IRolService
    {
        Task<ServiceResult> GetRolAll();
        Task<ServiceResult> GetEntityBy(int id);
        Task<ServiceResult> CreateRol(RolCreateDto createRolDto);
        Task<ServiceResult> UpdateRol(RolUpdateDto updateRolDto);
        Task<ServiceResult> RemoveRol(RolRemoveDto removeRolDto);
    }
}
