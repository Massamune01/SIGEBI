using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroFacade _libroFacade;
        private readonly ILogger<LibroService> _logger;
        private readonly ICacheService _cacheService;
        public LibroService(ILibroFacade facade, 
            ILogger<LibroService> logger,ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _libroFacade = facade;
        }

        public async Task<ServiceResult> CreateLibroAsync(LibroCreateDto libroCreateDto)
        {
            ServiceResult result = new ServiceResult();
 
            var libroResult = await _libroFacade.CreateLibroAsync(libroCreateDto);
            if (!libroResult.Success)
            {
                result.Success = false;
                result.Message = libroResult.Message;
                return result;
            }

            result.Success = true;
            result.Data = libroResult.Data;
            result.Message = libroResult.Message;
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> DeleteLibroAsync(Int64 id)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation("Deleting a book with ID: {BookId}", id);

            var libroResult = await _libroFacade.DeleteLibroAsync(id);
            if (!libroResult.Success)
            {
                result.Success = false;
                result.Message = libroResult.Message;
                return result;
            }

            result.Success = true;
            result.Message = "Book deleted successfully.";
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> GetAllLibrosAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Libros";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<Libro> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Libros retrieved from cache.";
                return result;
            }

            var libroResult = await _libroFacade.GetAllLibrosAsync();
            if (!libroResult.Success)
            {
                result.Success = false;
                result.Message = libroResult.Message;
                return result;
            }

            result.Success = true;
            result.Data = libroResult.Data;
            result.Message = "Books retrieved successfully.";
            _cacheService.Set(cacheKey, result.Data);
            return result;
        }

        public async Task<ServiceResult> GetLibroByIdAsync(Int64 id)
        {
            ServiceResult result = new ServiceResult();

            _logger.LogInformation("Retrieving book with ID: {BookId}", id);
            var libroResult = await _libroFacade.GetLibroByIdAsync(id);
            if (!libroResult.Success)
            {
                result.Success = false;
                result.Message = libroResult.Message;
                return result;
            }
            result.Success = true;
            result.Data = libroResult.Data;
            result.Message = libroResult.Message;
            _logger.LogInformation("Book retrieved successfully.");
            return result;
        }

        public async Task<ServiceResult> UpdateLibroAsync(LibroUpdateDto libroUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            
            _logger.LogInformation("Updating book with ID: {BookId}", libroUpdateDto.ISBN);

            var libroResult = await _libroFacade.UpdateLibroAsync(libroUpdateDto);
            if (!libroResult.Success)
            {
                result.Success = false;
                result.Message = libroResult.Message;
                return result;
            }

            result.Success = true;
            result.Message = libroResult.Message;
            result.Data = libroResult.Data;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }
    }
}
