using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Validators.Configuration.RolValidators
{
    public class RolUpdateValidator : IValidatorBase<RolUpdateDto>
    {
        private readonly IRolRepository _rolRepository;
        private readonly ILogger<RolUpdateValidator> _logger;

        public RolUpdateValidator(IRolRepository rolRepository, ILogger<RolUpdateValidator> logger)
        {
            _rolRepository = rolRepository;
            _logger = logger;
        }

        //No se implementa
        public Task<ValidationResult> ValidateCreate(RolUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateUpdate(RolUpdateDto entity)
        {
            ValidationResult validationresult = new ValidationResult();
            try
            {
                // Verificar si ya existe un rol con el mismo nombre
                var existingRoles = await _rolRepository.GetRolByName(entity.Rol);
                if (existingRoles != null && existingRoles.Any())
                {
                    validationresult.Errors.Add("A role with the same name already exists.");
                }
                // Verificar si el rol a actualizar existe
                var rolToUpdate = await _rolRepository.GetEntityBy(entity.Id);
                if (rolToUpdate == null)
                {
                    validationresult.Errors.Add("The role to update does not exist.");
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
    }
}
