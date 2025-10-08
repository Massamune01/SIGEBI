using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;
        private readonly IValidatorBase<ClienteCreateDto> _createValidator;
        private readonly IValidatorBase<ClienteUpdateDto> _updateValidator;

        public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger, IValidatorBase<ClienteUpdateDto> updateValidator, IValidatorBase<ClienteCreateDto> createvalidator)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
            _updateValidator = updateValidator;
            _createValidator = createvalidator;
        }

        public async Task<ServiceResult> CreateClienteAsync(ClienteCreateDto clienteCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business validations
                var clienteValidation = _createValidator.ValidateCreate(clienteCreateDto);
                /* Todavia no se implementa la validacion
                if (!bibliotecarioValidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }
                */

                _logger.LogInformation("Creating a client with name: {ClientName}", clienteCreateDto.Nombre);

                if (clienteCreateDto is null)
                {
                    result.Success = false;
                    result.Message = "The client data cannot be null.";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                Cliente cliente = new Cliente()
                {
                    Nombre = clienteCreateDto.Nombre,
                    Apellido = clienteCreateDto.Apellido,
                    Edad = clienteCreateDto.Edad,
                    Genero = clienteCreateDto.Genero,
                    Email = clienteCreateDto.Email,
                    Nacimiento = clienteCreateDto.Nacimiento ?? DateTime.Now,
                    RolId = clienteCreateDto.RolId,
                    CapacidadPrest = clienteCreateDto.CapacidadPrest ?? 0,
                    StatusCliente = clienteCreateDto.StatusCliente,
                    TotalDevoluciones = clienteCreateDto.TotalDevoluciones ?? 0,
                    TotalDevolRestrasadas = clienteCreateDto.TotalDevolRestrasadas ?? 0,
                    TotalPrestamos = clienteCreateDto.TotalPrestamos ?? 0,
                    PrestamosActivos = clienteCreateDto.PrestamosActivos ?? 0,
                    IdLgOpCliente = clienteCreateDto.IdLgOpCliente ?? 0,
                };

                var oClienteResult = await _clienteRepository.Save(cliente);

                if (oClienteResult is null)
                {
                    result.Success = false;
                    result.Message = "Failed to create client.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                result.Data = oClienteResult;
                result.Message = "Client created successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the client.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> DeleteClienteAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation($"Deleting Cliente with ID: {id}");
                var oClienteResul = await _clienteRepository.GetClienteById(id);
                if (oClienteResul is null)
                {
                    result.Success = false;
                    result.Message = "Bibliotecario not found.";
                    return result;
                }

                var Cliente = (Cliente?)oClienteResul.FirstOrDefault();

                var deleteResult = await _clienteRepository.Remove(Cliente);
                if (!deleteResult.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the bibliotecario.";
                    return result;
                }
                result.Success = true;
                result.Message = "Bibliotecario deleted successfully.";
                _logger.LogInformation(result.Message);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the bibliotecario.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetAllClientesAsync()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving all clients.");
                var oClienteResult = await _clienteRepository.GetAll();
                if (oClienteResult is null)
                {
                    result.Success = false;
                    result.Message = "No clients found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                result.Data = oClienteResult.Data;
                result.Message = "Clients retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving clients.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetClienteByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving client with ID: {ClientId}", id);
                var oClienteResult = await _clienteRepository.GetClienteById(id);
                if (oClienteResult is null)
                {
                    result.Success = false;
                    result.Message = "Client not found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                result.Data = oClienteResult;
                result.Message = "Client retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the client.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdateClienteAsync(ClienteUpdateDto clienteUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business validations
                var clienteValidation = _updateValidator.ValidateUpdate(clienteUpdateDto);
                /* Todavia no se implementa la validacion
                if (!bibliotecarioValidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }
                */

                _logger.LogInformation("Updating client with ID: {ClientId}", clienteUpdateDto.Id);
                if (clienteUpdateDto is null)
                {
                    result.Success = false;
                    result.Message = "The client data cannot be null.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                var existingClienteResult = await _clienteRepository.GetClienteById(clienteUpdateDto.Id);
                if (existingClienteResult is null)
                {
                    result.Success = false;
                    result.Message = "Client not found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                var existingCliente = (Cliente?)existingClienteResult.FirstOrDefault();

                Cliente cliente = new Cliente()
                {
                    Id = clienteUpdateDto.Id,
                    Nombre = clienteUpdateDto.Nombre,
                    Apellido = clienteUpdateDto.Apellido,
                    Edad = clienteUpdateDto.Edad,
                    Genero = clienteUpdateDto.Genero,
                    Email = clienteUpdateDto.Email,
                    Nacimiento = clienteUpdateDto.Nacimiento ?? DateTime.Now,
                    RolId = clienteUpdateDto.RolId,
                    CapacidadPrest = clienteUpdateDto.CapacidadPrest,
                    StatusCliente = clienteUpdateDto.StatusCliente,
                    TotalDevoluciones = clienteUpdateDto.TotalDevoluciones,
                    TotalDevolRestrasadas = clienteUpdateDto.TotalDevolRestrasadas,
                    TotalPrestamos = clienteUpdateDto.TotalPrestamos,
                };

                var updateResult = await _clienteRepository.Update(cliente);
                if (updateResult is null)
                {
                    result.Success = false;
                    result.Message = "Failed to update client.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Data = updateResult.Data;
                    result.Message = "Client updated successfully.";
                    _logger.LogInformation(result.Message);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the client.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
