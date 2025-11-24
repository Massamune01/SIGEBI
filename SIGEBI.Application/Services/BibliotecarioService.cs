using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class BibliotecarioService : IBibliotecarioService
    {
        private readonly ILogger<BibliotecarioService> _logger;
        private readonly IBibliotecarioFacade _bibliotecarioFacade;
        private readonly ICacheService _cacheService;

        public BibliotecarioService(IBibliotecarioFacade biblioFacade,
            ILogger<BibliotecarioService> logger, 
            ICacheService cacheService)
        {
            _logger = logger;
            _bibliotecarioFacade = biblioFacade;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreateBibliotecarioAsync(BibliotecarioCreateDto bibliotecarioCreateDto)
        {
            ServiceResult result = new ServiceResult();

            _logger.LogInformation("Creating Bibliotecario.");
            var biblioResult = await _bibliotecarioFacade.CreateBibliotecarioAsync(bibliotecarioCreateDto);
            if (!biblioResult.Success)
            {
                result.Success = false;
                result.Message = biblioResult.Message;
                _logger.LogError($"Error creating a Bibliotecario: {result.Message}");
            }
            result.Success = true;
            result.Data = biblioResult.Data;
            result.Message = biblioResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> DeleteBibliotecarioAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation($"Deleting bibliotecario with ID: {id}");

            var biblioResult = await _bibliotecarioFacade.DeleteBibliotecarioAsync(id);

            if (!biblioResult.Success)
            {
                result.Success = false;
                result.Message = biblioResult.Message;
                return result;
            }

            result.Success = true;
            result.Message = biblioResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> GetAllBibliotecariosAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Bibliotecario";

            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<Bibliotecarios> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Bibliotecarios retrieved from cache.";
                return result;
            }

            _logger.LogInformation("Retrieving all Bibliotecarios.");
            var biblioResult = await _bibliotecarioFacade.GetAllBibliotecariosAsync();
            if (!biblioResult.Success)
            {
                result.Success = false;
                result.Message = biblioResult.Message;
                _logger.LogError($"Error retrieving all biblio: {result.Message}");
            }
            _cacheService.Set(cacheKey, biblioResult.Data);

            result.Success = true;
            result.Data = biblioResult.Data;
            result.Message = biblioResult.Message;
            _logger.LogInformation(result.Message);
                
            return result;
        }

        public async Task<ServiceResult> GetBibliotecarioByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();

            _logger.LogInformation($"Retrieving bibliotecario with ID: {id}");

            var bilioResult = await _bibliotecarioFacade.GetBibliotecarioByIdAsync(id);
            if(!bilioResult.Success)
            {
                result.Success = false;
                result.Message = bilioResult.Message;
                _logger.LogError($"Error retrieving a Bibliotecario: {result.Message}");
                return result;
            }

            result.Success = true;
            result.Data = bilioResult.Data;
            result.Message = bilioResult.Message;
            _logger.LogInformation(result.Message);
            return result;
  
        }

        public async Task<ServiceResult> UpdateBibliotecarioAsync(BibliotecarioUpdateDto bibliotecarioUpdateDto)
        {
            ServiceResult result = new ServiceResult();

            var biblioResult = await _bibliotecarioFacade.UpdateBibliotecarioAsync(bibliotecarioUpdateDto);
                
            if (!biblioResult.Success)
            {
                result.Success = false;
                result.Message = biblioResult.Message;
                return result;
            }
            result.Success = true;
            result.Data = biblioResult.Data;
            result.Message = biblioResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }
    }
}
