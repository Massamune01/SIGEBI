using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Validators.Configuration.LogOpValidators
{
    public class LogOperationUpdateValidator : IValidatorBase<UpdateLogOperationDto>
    {
        private readonly ILogOperationsRepository _logOperationsRepository;
        private readonly ILogger<LogOperationUpdateValidator> _logger;

        public LogOperationUpdateValidator(ILogOperationsRepository logOperationsRepository, ILogger<LogOperationUpdateValidator> logger)
        {
            _logOperationsRepository = logOperationsRepository;
            _logger = logger;
        }

        //No se implementa
        public Task<ValidationResult> ValidateCreate(UpdateLogOperationDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateUpdate(UpdateLogOperationDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields

                // Check if Description is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Descripcion))
                {
                    validationResult.AddError("Description is required.");
                }

                //Check if LastUpdateBy is not in the future
                if (entity.LastUpdateBy > DateTime.Now)
                {
                    validationResult.AddError("LastUpdateBy cannot be in the future.");
                }

                // Check if UpdateBy is not in the future
                if (entity.UpdateBy > DateTime.Now)
                {
                    validationResult.AddError("UpdateBy cannot be in the future.");
                }

                // Additional validations can be added here
                return validationResult;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during LogOperation creation validation");
                validationResult.AddError("An unexpected error occurred during validation.");
                return validationResult;
            }
        }
    }
}
