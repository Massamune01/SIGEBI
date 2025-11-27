using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Facades_Classes.Configuration
{
    public class LogOperationFacade : ILogOperationFacade
    {
        private readonly ILogOperationsRepository _logOperationsRepository;
        private readonly IValidatorBase<LogOperationsDto> _Validator;
        private readonly ILogger<LogOperationFacade> _logger;

        public LogOperationFacade(ILogOperationsRepository logOperationsRepository, 
            IValidatorBase<LogOperationsDto> validator, ILogger<LogOperationFacade> logger)
        {
            _logOperationsRepository = logOperationsRepository;
            _Validator = validator;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateLogOperationsAsync(CreateLogOperationDto logOpCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business validations
                _logger.LogInformation("Validating LogOperations");

                LogOperationsDto logOperationsDto = new LogOperationsDto()
                {
                    Entity = logOpCreateDto.Entity,
                    Fecha = logOpCreateDto.Fecha,
                    TypeOperation = logOpCreateDto.TypeOperation,
                    Descripcion = logOpCreateDto.Descripcion,
                    StatusOp = (Status)logOpCreateDto.StatusOp
                };

                var createValidator = await _Validator.Validate(logOperationsDto, 1);
                if (!createValidator.IsValid)
                {
                    result.Success = false;
                    result.Message = createValidator.Errors.FirstOrDefault();
                    return result;
                }


                _logger.LogInformation("Creating a log operation for entity: {Entity}", logOpCreateDto.Entity);
                if (logOpCreateDto is null)
                {
                    result.Success = false;
                    result.Message = "The log operation data cannot be null.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                LogOperations logOperation = new LogOperations()
                {
                    Entity = logOpCreateDto.Entity,
                    Fecha = logOpCreateDto.Fecha,
                    TypeOperation = logOpCreateDto.TypeOperation,
                    Descripcion = logOpCreateDto.Descripcion,
                    StatusOp = (Status)logOpCreateDto.StatusOp
                };
                var createdLogOperation = await _logOperationsRepository.Save(logOperation);
                if (createdLogOperation is null)
                {
                    result.Success = false;
                    result.Message = "Failed to create the log operation.";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                result.Success = true;
                result.Data = createdLogOperation;
                result.Message = "Log operation created successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the log operation.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> DeleteLogOperationsAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Deleting log operation with ID: {Id}", id);
                var oLogOperation = await _logOperationsRepository.GetLogOpById(id);
                if (oLogOperation is null)
                {
                    result.Success = false;
                    result.Message = "Log operation not found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                var logOperations = oLogOperation.FirstOrDefault();

                var deleteResult = await _logOperationsRepository.Remove(logOperations);
                if (!deleteResult.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the log operation.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                result.Message = "Log operation deleted successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the log operation.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetAllLogOperationsAsync()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving all log operations.");
                var logOperations = await _logOperationsRepository.GetAll();
                if (logOperations is null || logOperations.Data is null)
                {
                    result.Success = false;
                    result.Message = "No log operations found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }

                List<LogOperations> logOpList = logOperations.Data;

                result.Success = true;
                result.Data = logOpList;
                result.Message = "Log operations retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving log operations.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetLogOperationsByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving log operation with ID: {Id}", id);
                var oLogOperation = await _logOperationsRepository.GetLogOpById(id);
                if (oLogOperation is null)
                {
                    result.Success = false;
                    result.Message = "Log operation not found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                result.Data = oLogOperation.FirstOrDefault();
                result.Message = "Log operation retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the log operation.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdateLogOperationsAsync(UpdateLogOperationDto logOpUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business validations
                //Business validations
                _logger.LogInformation("Validating LogOperations");

                LogOperationsDto logOperationsDto = new LogOperationsDto()
                {
                    Descripcion = logOpUpdateDto.Descripcion,
                    StatusOp = (Status)logOpUpdateDto.StatusOp
                };

                var updateValidator = await _Validator.Validate(logOperationsDto, 2);

                if (!updateValidator.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", updateValidator.Errors);
                    return result;
                }


                _logger.LogInformation("Updating log operation with ID: {Id}", logOpUpdateDto.IdOp);
                if (logOpUpdateDto is null)
                {
                    result.Success = false;
                    result.Message = "The log operation data cannot be null.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                var oLogOperation = await _logOperationsRepository.GetLogOpById(logOpUpdateDto.IdOp);
                if (oLogOperation is null)
                {
                    result.Success = false;
                    result.Message = "Log operation not found.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                var logOperation = new LogOperations()
                {
                    IdOp = logOpUpdateDto.IdOp,
                    Descripcion = logOpUpdateDto.Descripcion,
                    UpdateBy = logOpUpdateDto.UpdateBy,
                    StatusOp = logOpUpdateDto.StatusOp,
                    LastUpdateBy = logOpUpdateDto.LastUpdateBy
                };
                var updatedLogOperationResult = await _logOperationsRepository.Update(logOperation);
                if (!updatedLogOperationResult.Success || updatedLogOperationResult.Data is null)
                {
                    result.Success = false;
                    result.Message = "Failed to update the log operation.";
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                result.Data = updatedLogOperationResult.Data;
                result.Message = "Log operation updated successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the log operation.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
