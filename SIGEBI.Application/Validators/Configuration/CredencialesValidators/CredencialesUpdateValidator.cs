using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.CredencialesValidators
{
    public class CredencialesUpdateValidator : IValidatorBase<CredencialesUpdateDto>
    {
        private readonly ICredencialesRepository _credencialesRepository;
        private readonly ILogger<CredencialesUpdateValidator> _logger;

        public CredencialesUpdateValidator(ICredencialesRepository credencialesRepository,
            ILogger<CredencialesUpdateValidator> logger)
        {
            _credencialesRepository = credencialesRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> ValidateUpdate(CredencialesUpdateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {

                // Basic validation: Check for required fields

                // Check if Usuario is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Usuario))
                {
                    validationResult.AddError("Usuario is required.");
                }

                // Check if Usuario is unique
                var existingCredenciales = await _credencialesRepository.GetCredencialesByUsuario(entity.Usuario);
                if (existingCredenciales.Success)
                {
                    validationResult.AddError($"Usuario '{entity.Usuario}' is already in use.");
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during Credenciales creation validation.");
                validationResult.AddError("An unexpected error occurred during validation.");
                return validationResult;
            }
        }


        //No se implementa
        public Task<ValidationResult> ValidateCreate(CredencialesUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
