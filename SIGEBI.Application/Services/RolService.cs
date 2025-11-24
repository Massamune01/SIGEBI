using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class RolService : IRolService
    {
        private readonly ILogger<RolService> _logger;
        private readonly ICacheService _cacheService;
        private readonly IRolesFacade _rolFacade;


        public RolService(IRolesFacade facade, 
            ILogger<RolService> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _rolFacade = facade;
        }

        public async Task<ServiceResult> CreateRol(RolCreateDto createRolDto)
        {
            ServiceResult result = new ServiceResult();

            var rolResult = await _rolFacade.CreateRol(createRolDto);
            if (!rolResult.Success)
            {
                result.Success = false;
                result.Message = rolResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = rolResult.Data;
            result.Message = rolResult.Message;
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> RemoveRol(RolRemoveDto removeRolDto)
        {
            ServiceResult result = new ServiceResult();
            
            var rolResult = await _rolFacade.RemoveRol(removeRolDto);
            if(!rolResult.Success)
            {
                result.Success = false;
                result.Message = rolResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Message = rolResult.Message;
            result.Data = rolResult.Data;
            _cacheService.ClearKeys();
            _logger.LogInformation("Remove Completed Succesfuly");
            return result;
        }

        public async Task<ServiceResult> UpdateRol(RolUpdateDto updateRolDto)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation($"Updating Rol with Id: {updateRolDto.Id}");

            var rolResult = await _rolFacade.UpdateRol(updateRolDto);
            if(!rolResult.Success)
            {
                result.Success = false;
                result.Message = rolResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Message = rolResult.Message;
            result.Data = rolResult.Data;
            _cacheService.ClearKeys();
            _logger.LogInformation($"Updating Completed Succesfuly");
            return result;
           
        }

        public async Task<ServiceResult> GetRolAll()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Roles";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<RolGetModel> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Roles retrieved from cache.";
                return result;
            }

            _logger.LogInformation("Retrieving all roles.");

            var rolResult = await _rolFacade.GetRolAll();
            if(!rolResult.Success)
            {
                result.Success = false;
                result.Message = rolResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = rolResult.Data;
            result.Message = rolResult.Message;
            _cacheService.Set(cacheKey, result.Data);
            _logger.LogInformation("Get all roles Completed Succesfuly.");
            return result;
        }

        public async Task<ServiceResult> GetEntityBy(int id)
        {
            ServiceResult result = new ServiceResult();

            var rolResult = await _rolFacade.GetEntityBy(id);
            if(!rolResult.Success)
            {
                result.Success = false;
                result.Message = rolResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Message = rolResult.Message;
            result.Data = rolResult.Data;

            return result;
        }
    }
}
