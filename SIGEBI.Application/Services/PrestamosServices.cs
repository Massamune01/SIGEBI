using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class PrestamosServices : IPrestamosService
    {
        private readonly ILogger<PrestamosServices> _logger;
        private readonly ICacheService _cacheService;
        private readonly IPrestamoFacade _prestamoFacade;

        public PrestamosServices( IPrestamoFacade facade,
            ILogger<PrestamosServices> logger, 
            ICacheService cacheService)
        {
            _prestamoFacade = facade;
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> GetLibroWithTituloAndISBN()
        {
            ServiceResult result = new ServiceResult();

            var prestResult = await _prestamoFacade.GetLibroWithTituloAndISBN();

            if(!prestResult.Success)
            {
                result.Success = false;
                result.Message = prestResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = prestResult.Data;
            result.Message = prestResult.Message;
            _logger.LogInformation("Prestamos Retrieving Succesfuly");
            return result;
            
        }

        public async Task<ServiceResult> CreatePrestamoAsync(PrestamoCreateDto prestamoCreateDto)
        {
            ServiceResult result = new ServiceResult();
            
            var prestResult = await _prestamoFacade.CreatePrestamoAsync(prestamoCreateDto);
            if (!prestResult.Success)
            {
                result.Success = false;
                result.Message = prestResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = prestResult.Data;
            result.Message = prestResult.Message;
            _logger.LogInformation("Prestamos Created Succesfuly");
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> DeletePrestamoAsync(int id)
        {
            ServiceResult result = new ServiceResult();
           
            var prestResult = await _prestamoFacade.DeletePrestamoAsync(id);
            if(!prestResult.Success)
            {
                result.Success = false;
                result.Message = prestResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Message = prestResult.Message;
            _cacheService.ClearKeys();
            _logger.LogInformation("Prestamos Deleted Succesfuly");
            return result;
        }

        public async Task<ServiceResult> GetAllPrestamosAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Prestamos";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<PrestamoDto> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Prestamos retrieved from cache.";
                return result;
            }

            var prestResult = await _prestamoFacade.GetAllPrestamosAsync();
            if(!prestResult.Success)
            {
                result.Success = false;
                result.Message = prestResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = prestResult.Data;
            result.Message = prestResult.Message;
            _cacheService.Set(cacheKey, prestResult.Data);
            _logger.LogInformation("Prestamos Retrieved Succesfuly");
            return result; 
        }

        public async Task<ServiceResult> GetPrestamoByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();

            var prestResult = await _prestamoFacade.GetPrestamoByIdAsync(id);
            if(!prestResult.Success)
            {
                result.Success = false;
                result.Message = prestResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = prestResult.Data;
            result.Message = prestResult.Message;
            _logger.LogInformation("Prestamo Retrieved Succesfuly");
            return result;
           
        }

        public async Task<ServiceResult> UpdatePrestamoAsync(PrestamoUpdateDto prestamoUpdateDto)
        {
            ServiceResult result = new ServiceResult();

            var prestResult = await _prestamoFacade.UpdatePrestamoAsync(prestamoUpdateDto);
            if(!prestResult.Success)
            {
                result.Success = false;
                result.Message = prestResult.Message;
                _logger.LogError($"Error: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = prestResult.Data;
            result.Message = prestResult.Message;
            _cacheService.ClearKeys();
            _logger.LogInformation("Prestamo Updated Succesfuly");
            return result;
           
        }
    }
}
