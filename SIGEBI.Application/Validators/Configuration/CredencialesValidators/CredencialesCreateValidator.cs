using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;

namespace SIGEBI.Application.Validators.Configuration.CredencialesValidators
{
    public class CredencialesCreateValidator : IValidatorBase<CredencialesCreateDto>
    {
        private readonly ILogger<CredencialesCreateValidator> _logger;
        private readonly ICredencialesRepository _credencialesRepository;

        public CredencialesCreateValidator(ILogger<CredencialesCreateValidator> logger,
            ICredencialesRepository credencialesRepository)
        {
            _logger = logger;
            _credencialesRepository = credencialesRepository;
        }

        public async Task<ValidationResult> ValidateCreate(CredencialesCreateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try {                 
                
                // Basic validation: Check for required fields

                // Check if Usuario is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Usuario))
                {
                    validationResult.AddError("Usuario is required.");
                }
                // Check if Password is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Password))
                {
                    validationResult.AddError("Password is required.");
                }

                // Check if ClienteId exists in the database
                var clienteExists = await _credencialesRepository.ClienteExist(entity.ClienteId);
                if(clienteExists.Success == false)
                {
                    validationResult.AddError($"Cliente with Id {entity.ClienteId} does not exist.");
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
        public Task<ValidationResult> ValidateUpdate(CredencialesCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
