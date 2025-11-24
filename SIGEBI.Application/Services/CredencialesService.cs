using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class CredencialesService : ICredencialesService
    {
        private readonly ICredencialesFacade _credeFacade;
        private readonly ILogger<CredencialesService> _logger;
        private readonly ICacheService _cacheService;

        public CredencialesService(ICredencialesFacade facade, 
            ILogger<CredencialesService> logger, ICacheService cacheService)
        {
            _credeFacade = facade;
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreateCredencialesAsync(CredencialesCreateDto createCredencialesDto)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation("Creating new Credenciales for ClienteId: {ClienteId}", createCredencialesDto.ClienteId);

            var credeResult = await _credeFacade.CreateCredencialesAsync(createCredencialesDto);
            if (!credeResult.Success)
            {
                result.Success = false;
                result.Message = credeResult.Message;
                _logger.LogError($"ERROR: {result.Message} Creating a Credenciales for {createCredencialesDto.ClienteId}");
            }
            result.Success = true;
            result.Data = credeResult.Data;
            result.Message = credeResult.Message;
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> GetCredencialesAllAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Credenciales";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<CredencialesGetModel> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Credenciales retrieved from cache.";
                return result;
            }

            var credeResult = await _credeFacade.GetCredencialesAllAsync();

            if(!credeResult.Success)
            {
                result.Success = false;
                result.Message = credeResult.Message;
                _logger.LogError($"ERROR Retrieving All Credenciales. {result.Message}");
                return result;
            }
            result.Success = true;
            result.Data = credeResult.Data;
            result.Message = credeResult.Message;
            _cacheService.Set(cacheKey, result.Data);
            _logger.LogInformation("Credenciales Retrive Succesfuly");
            return result;
        }

        public async Task<ServiceResult> GetCredencialesByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            
            var credeResult = await _credeFacade.GetCredencialesByIdAsync(id);
            if (!credeResult.Success)
            {
                result.Success = false;
                result.Message = credeResult.Message;
                _logger.LogError($"ERROR Retrieving Credenciales by ID: {id}. {result.Message}");
                return result;
            }   
            result.Success = true;
            result.Data = credeResult.Data;
            result.Message = credeResult.Message;
            _logger.LogInformation("Credenciales Retrive by ID: {CredID} Succesfuly", id);

            return result;
        }

        public async Task<ServiceResult> RemoveCredencialesAsync(CredencialesRemoveDto removeCredencialesDto)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation("Removing Credenciales with ID: {CredId}", removeCredencialesDto.Id);

            var credeResult = await _credeFacade.RemoveCredencialesAsync(removeCredencialesDto);

            if (!credeResult.Success)
            {
                result.Success = false;
                result.Message = credeResult.Message;
                return result;
            }
            result.Success = true;
            result.Data = credeResult.Data;
            result.Message = credeResult.Message;
            _cacheService.ClearKeys();

            return result;
        }

        public async Task<ServiceResult> UpdateCredencialesAsync(CredencialesUpdateDto updateCredencialesDto)
        {
            ServiceResult result = new ServiceResult();

            _logger.LogInformation("Validating update for Credenciales with ID: {CredID}", updateCredencialesDto.Id);

            var credeResult = await _credeFacade.UpdateCredencialesAsync(updateCredencialesDto);

            if (!credeResult.Success)
            {
                result.Success = false;
                result.Message = credeResult.Message;
                _logger.LogError($"ERROR: {result.Message} Updating Credenciales with ID: {updateCredencialesDto.Id}");
                return result;
            }

            result.Success = true;
            result.Message = credeResult.Message;
            result.Data = credeResult.Data;
            _logger.LogInformation("Credenciales with ID: {CredID} updated successfully", updateCredencialesDto.Id);
            _cacheService.ClearKeys();
            return result;
        }
    }
}
