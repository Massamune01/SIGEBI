using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class AdminServices : IAdminService
    {
        private readonly ILogger<AdminServices> _logger;
        private readonly IAdminFacade _adminFacade;
        private readonly ICacheService _cacheService;

        public AdminServices(IAdminFacade adminFacade,IAdminRepository adminRepository, ILogger<AdminServices> logger,  
            ICacheService cacheService)
        {
            _logger = logger;
            _adminFacade = adminFacade;
            _cacheService = cacheService;

        }

        public async Task<ServiceResult> CreateAdminAsync(AdminCreateDto adminCreateDto)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation("Creating an admin");
            var adminResult = await _adminFacade.CreateAdminAsync(adminCreateDto);

            if(!adminResult.Success)
            {
                result.Success = false;
                result.Message = adminResult.Message;
                return result;
            }
            result.Success = true;
            result.Data = adminResult.Data;
            result.Message = adminResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();

            return result;
        }

        public async Task<ServiceResult> DeleteAdminAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            
            var adminResult = await _adminFacade.DeleteAdminAsync(id);
            if(!adminResult.Success)
            {
                result.Success = false;
                result.Message = adminResult.Message;
                return result;
            }
            result.Success = true;
            result.Message = adminResult.Message;
            _cacheService.ClearKeys();

            return result;
        }

        public async Task<ServiceResult> GetAdminByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();

            var adminResult = await _adminFacade.GetAdminByIdAsync(id);
            if(!adminResult.Success)
            {
                result.Success = false;
                result.Message = adminResult.Message;
                return result;
            }
            result.Success = true;
            result.Data = adminResult.Data;
            result.Message = adminResult.Message;
            return result;
        }

        public async Task<ServiceResult> GetAllAdminAsync()
        {
            const string cacheKey = "ALL_ADMIN";
            ServiceResult result = new ServiceResult();

            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<Admin> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Admins retrieved from cache.";
                return result;
            }
            else
            {
                var adminsResult = await _adminFacade.GetAllAdminAsync();
                if (!adminsResult.Success)
                {
                    result.Success = false;
                    result.Message = adminsResult.Message;
                    return result;
                }
                _cacheService.Set(cacheKey, adminsResult.Data);
                result.Success = true;
                result.Data = adminsResult.Data;
                result.Message = "Admins retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdateAdminAsync(AdminUpdateDto adminUpdateDto)
        {
            ServiceResult result = new ServiceResult();

            var adminResult = await _adminFacade.UpdateAdminAsync(adminUpdateDto);

            if (!adminResult.Success)
            {
                result.Success = false;
                result.Message = adminResult.Message;
                return result;
            }
            result.Success = true;
            result.Data = adminResult.Data;
            result.Message = adminResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

    }
}
