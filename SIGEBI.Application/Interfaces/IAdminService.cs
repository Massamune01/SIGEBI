using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResult> GetAllAdminAsync();
        Task<ServiceResult> GetAdminByIdAsync(int id);
        Task<ServiceResult> CreateAdminAsync(AdminCreateDto adminCreateDto);
        Task<ServiceResult> UpdateAdminAsync(AdminUpdateDto adminUpdateDto);
        Task<ServiceResult> DeleteAdminAsync(int id);

    }
}