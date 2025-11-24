using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class LogOperationsService : ILogOperationsService
    {
        private readonly ILogOperationFacade _logOpFacade;
        private readonly ILogger<LogOperationsService> _logger;
        private readonly ICacheService _cacheService;

        public LogOperationsService(ILogOperationFacade facade, 
            ILogger<LogOperationsService> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _logOpFacade = facade;
        }

        public async Task<ServiceResult> CreateLogOperationsAsync(CreateLogOperationDto logOpCreateDto)
        {
            ServiceResult result = new ServiceResult();

            var logOpResult = await _logOpFacade.CreateLogOperationsAsync(logOpCreateDto);
            if (!logOpResult.Success)
            {
                result.Success = false;
                result.Message = logOpResult.Message;
            }

            result.Success = true;
            result.Data = logOpResult;
            result.Message = logOpResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> DeleteLogOperationsAsync(int id)
        {
            ServiceResult result = new ServiceResult();

            var logOpResult = await _logOpFacade.DeleteLogOperationsAsync(id);
            if (!logOpResult.Success)
            {
                result.Success = false;
                result.Message = logOpResult.Message;
                _logger.LogInformation($"Error deleting {id}, Error: {result.Message}");
            }

            result.Success = true;
            result.Message = "Log operation deleted successfully.";
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> GetAllLogOperationsAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_LogOps";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<LogOperations> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "LogOps retrieved from cache.";
                return result;
            }

           var logOpResult = await _logOpFacade.GetAllLogOperationsAsync();
            if (!logOpResult.Success)
            {
                result.Success = false;
                result.Message = logOpResult.Message;
                _logger.LogError(result.Message);
                return result;
            }

            result.Success = true;
            result.Data = logOpResult.Data;
            result.Message = logOpResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.Set(cacheKey, result.Data);
            return result;
        }

        public async Task<ServiceResult> GetLogOperationsByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
           
            var logOpResult = await _logOpFacade.GetLogOperationsByIdAsync(id);
            if(!logOpResult.Success)
            {
                result.Success = false;
                result.Message = logOpResult.Message;
                _logger.LogError(result.Message);
            }

            result.Success = true;
            result.Data = logOpResult.Data;
            result.Message = logOpResult.Message;
            _logger.LogInformation(result.Message);
            return result;
        }

        public async Task<ServiceResult> UpdateLogOperationsAsync(UpdateLogOperationDto logOpUpdateDto)
        {
            ServiceResult result = new ServiceResult();

            var logOpResult = await _logOpFacade.UpdateLogOperationsAsync(logOpUpdateDto);
            if(!logOpResult.Success)
            {
                result.Success = false;
                result.Message = logOpResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = logOpResult.Data;
            result.Message = logOpResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }
    }
}
