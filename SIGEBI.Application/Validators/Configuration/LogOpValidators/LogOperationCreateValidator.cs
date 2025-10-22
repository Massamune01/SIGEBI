using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Validators.Configuration.LogOpValidators
{
    public class LogOperationCreateValidator : IValidatorBase<CreateLogOperationDto>
    {
        private readonly ILogOperationsRepository _logOperationsRepository;
        private readonly ILogger<LogOperationCreateValidator> _logger;

        public LogOperationCreateValidator(ILogOperationsRepository logOperationsRepository, ILogger<LogOperationCreateValidator> logger)
        {
            _logOperationsRepository = logOperationsRepository;
            _logger = logger;
        }


        public async Task<ValidationResult> ValidateCreate(CreateLogOperationDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields
                // Check if Entity is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Entity))
                {
                    validationResult.AddError("Entity is required.");
                }

                // Check if Description is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Descripcion))
                {
                    validationResult.AddError("Description is required.");
                }

                // Check if TypeOperation is not null or empty
                if (string.IsNullOrWhiteSpace(entity.TypeOperation))
                {
                    validationResult.AddError("TypeOperation is required.");
                }

                // Check if Fecha is not in the future
                if (entity.Fecha > DateTime.Now)
                {
                    validationResult.AddError("Fecha cannot be in the future.");
                }

                return validationResult;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during LogOperation creation validation");
                validationResult.AddError("An unexpected error occurred during validation.");
                return validationResult;
            }
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(CreateLogOperationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
