using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteFacade _clienteFacade;
        private readonly ILogger<ClienteService> _logger;
        private readonly IValidatorBase<ClienteDto> _Validator;
        private readonly ICacheService _cacheService;

        public ClienteService(IClienteFacade clienteFacade,IClienteRepository clienteRepository, ILogger<ClienteService> logger, 
            IValidatorBase<ClienteDto> validator, ICacheService cacheService)
        {
            _clienteRepository = clienteRepository;
            _clienteFacade = clienteFacade;
            _logger = logger;
            _Validator = validator;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreateClienteAsync(ClienteCreateDto clienteCreateDto)
        {
            ServiceResult result = new ServiceResult();

            _logger.LogInformation("Creating a Cliente");

            var clienteResult = await _clienteFacade.CreateClienteAsync(clienteCreateDto);
                
            if (!clienteResult.Success)
            {
                result.Success = false;
                result.Message = clienteResult.Message;
                _logger.LogWarning(result.Message);
                return result;
            }
            result.Success = true;
            result.Data = clienteResult.Data;
            result.Message = clienteResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> DeleteClienteAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation($"Deleting Cliente with ID: {id}");

            var clienteResult = await _clienteFacade.DeleteClienteAsync(id);

            if (!clienteResult.Success)
            {
                result.Success = false;
                result.Message = clienteResult.Message;
                return result;
            }
            result.Success = true;
            result.Message = clienteResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }

        public async Task<ServiceResult> GetAllClientesAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Clientes";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<Cliente> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Clientes retrieved from cache.";
                return result;
            }

            var clienteResult = await _clienteFacade.GetAllClienteAsync();
            if (!clienteResult.Success)
            {
                result.Success = false;
                result.Message = clienteResult.Message;
                _logger.LogError($"Error retrieving all Clientes. {result.Message}");
            }

            _cacheService.Set(cacheKey, clienteResult.Data);

            result.Success = true;
            result.Data = clienteResult.Data;
            result.Message = clienteResult.Message;
            _logger.LogInformation(result.Message);
            return result;
            
        }
        public async Task<ServiceResult> GetClienteByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            _logger.LogInformation("Retrieving client with ID: {ClientId}", id);

            var clienteResult = await _clienteFacade.GetClienteByIdAsync(id);
            
            if (!clienteResult.Success)
            {
                result.Success = false;
                result.Message = clienteResult.Message;
                _logger.LogWarning(result.Message);
                return result;
            }
            result.Success = true;
            result.Data = clienteResult.Data;
            result.Message = clienteResult.Message;
            _logger.LogInformation(result.Message);
            return result;
    }

        public async Task<ServiceResult> UpdateClienteAsync(ClienteUpdateDto clienteUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            //Business validations
            _logger.LogInformation($"Updating Cliente with ID: {clienteUpdateDto.Id}");

            var clienteResult = await _clienteFacade.UpdateClienteAsync(clienteUpdateDto);
            if (!clienteResult.Success)
            {
                result.Success = false;
                result.Message = clienteResult.Message;
                _logger.LogError($"Error Updating Cliente> {result.Message}");
            }

            result.Success = true;
            result.Data = clienteResult.Data;
            result.Message = clienteResult.Message;
            _logger.LogInformation(result.Message);
            _cacheService.ClearKeys();
            return result;
        }
    }
}
