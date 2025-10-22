using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Application.Validators.Configuration.PrestamosValidators
{
    public class PrestamoUpdateValidator : IValidatorBase<PrestamoUpdateDto>
    {
        private readonly IPrestamosRepository _prestamosRepository;
        private readonly ILogger<PrestamoUpdateValidator> _logger;  

        public PrestamoUpdateValidator(IPrestamosRepository prestamosRepository, ILogger<PrestamoUpdateValidator> logger)
        {
            _prestamosRepository = prestamosRepository;
            _logger = logger;
        }

        //No se implementa
        public Task<ValidationResult> ValidateCreate(PrestamoUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateUpdate(PrestamoUpdateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                _logger.LogInformation("Validating PrestamoUpdate");
                // Basic validation: Check for required fields
                // Check DateWasDevol is not default
                if (entity.DateWasDevol == default)
                {
                    validationResult.AddError("DateWasDevol is required.");
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during PrestamoCreateDto validation.");
                validationResult.AddError("An unexpected error occurred during validation.");
                return validationResult;
            }
        }
    }
}
