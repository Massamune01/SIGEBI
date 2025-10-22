using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.RolValidators
{
    public class RolCreateValidator : IValidatorBase<RolCreateDto>
    {
        private readonly IRolRepository _rolRepository;
        private readonly ILogger<RolCreateValidator> _logger;

        public RolCreateValidator(IRolRepository rolRepository, ILogger<RolCreateValidator> logger)
        {
            _rolRepository = rolRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> ValidateCreate(RolCreateDto entity)
        {
            ValidationResult validationresult = new ValidationResult();
            try
            {
                // Verificar si ya existe un rol con el mismo nombre
                var existingRoles = await _rolRepository.GetRolByName(entity.Nombre);
                if (existingRoles != null && existingRoles.Any())
                {
                    validationresult.Errors.Add("A role with the same name already exists.");
                }

                return validationresult;
            }
            catch (Exception ex)
            {
                validationresult.Errors.Add("An error occurred during validation.");
                _logger.LogError(ex, "Error during role creation validation.");
                return validationresult;
            }
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(RolCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
