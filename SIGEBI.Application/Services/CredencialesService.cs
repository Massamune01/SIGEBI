using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Services
{
    public class CredencialesService : ICredencialesService
    {
        private readonly ICredencialesRepository _credencialesRepository;
        private readonly ILogger<CredencialesService> _logger;

        public CredencialesService(ICredencialesRepository credencialesRepository, ILogger<CredencialesService> logger)
        {
            _credencialesRepository = credencialesRepository;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateCredenciales(CredencialesCreateDto createCredencialesDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if(createCredencialesDto is null)
                {
                    result.Success = false;
                    result.Message = "The credentials data cannot be null.";
                    return result;
                }
                Credenciales credenciales = new Credenciales()
                {
                    ClienteId = createCredencialesDto.ClienteId,
                    Usuario = createCredencialesDto.Usuario,
                    PasswordHash = createCredencialesDto.Password
                };

                var oResultCredenciales = await _credencialesRepository.Save(credenciales);
                result.Success = true;
                result.Data = oResultCredenciales;
                result.Message = "Credenciales created successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the credentials.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetCredencialesAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving all credentials.");
                var oCredencialesList = await _credencialesRepository.GetAll();
                if(oCredencialesList is null)
                {
                    result.Success = false;
                    result.Message = "Credenciales not found";
                    return result;
                }
                result.Success = true;
                result.Data = oCredencialesList;
                result.Message = "Credenciales retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving credentials.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetCredencialesById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving Credenciales with ID: {CredId}", id);
                var oCredenciales = await _credencialesRepository.GetCredencialesById(id);
                if (oCredenciales is not null) 
                {
                    result.Success = true;
                    result.Message = "Retrieving Credenciales Succesfully.";
                    result.Data = oCredenciales;
                }
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the credenciales.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> RemoveCredenciales(CredencialesRemoveDto removeCredencialesDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Removing Credenciales with ID: {CredId}", removeCredencialesDto.Id);
                if(removeCredencialesDto is null || removeCredencialesDto.Id <= 0)
                {
                    result.Success = false;
                    result.Message = "The credentials data cannot be null or invalid.";
                    return result;
                }
                var oCredenciales = await _credencialesRepository.GetEntityBy(removeCredencialesDto.Id);
                if (oCredenciales is null)
                {
                    result.Success = false;
                    result.Message = "Credenciales not found.";
                    return result;
                }

                Credenciales credenciales = new Credenciales()
                {
                    Id = removeCredencialesDto.Id
                };

                var oResultCredenciales = await _credencialesRepository.Remove(credenciales);
                if (!oResultCredenciales.Success)
                {
                    result.Success = false;
                    result.Message = oResultCredenciales.Message;
                    return result;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while removing the credentials.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> UpdateCredenciales(CredencialesUpdateDto updateCredencialesDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Updating Credenciales with ID: {CredID}", updateCredencialesDto.Id);
                if (updateCredencialesDto is null || updateCredencialesDto.Id <= 0)
                {
                    result.Success = false;
                    result.Message = "The credentials data cannot be null or invalid.";
                    return result;
                }
                var oCredenciales = await _credencialesRepository.GetEntityBy(updateCredencialesDto.Id);
                if (oCredenciales is null)
                {
                    result.Success = false;
                    result.Message = "Credenciales not found.";
                    return result;
                }
                Credenciales credenciales = new Credenciales()
                {
                    Id = updateCredencialesDto.Id,
                    Usuario = updateCredencialesDto.Usuario
                };
                var oResultCredenciales = await _credencialesRepository.Update(credenciales);
                if (!oResultCredenciales.Success)
                {
                    result.Success = false;
                    result.Message = oResultCredenciales.Message;

                    return result;
                }

                result.Success = true;
                result.Message = "Update Credenciales Succesfully.";
                result.Data = oResultCredenciales.Data;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the credentials.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
