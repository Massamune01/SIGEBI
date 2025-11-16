using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.RolValidators
{
    public class RolValidator : IValidatorBase<RolDto>
    {
        private readonly IRolRepository _rolRepository;
        private readonly ILogger<RolValidator> _logger;

        public RolValidator(IRolRepository rolRepository, ILogger<RolValidator> logger)
        {
            _rolRepository = rolRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Validate(RolDto entity, int opcion)
        {
            ValidationResult validationresult = new ValidationResult();
            try
            {
                if(opcion == 1) // Crear
                {
                    if (string.IsNullOrWhiteSpace(entity.Rol))
                    {
                        validationresult.Errors.Add("The role name is required.");
                    }
                    // Verificar si ya existe un rol con el mismo nombre
                    var existingRoles = await _rolRepository.GetRolByName(entity.Rol);
                    if (existingRoles != null && existingRoles.Any())
                    {
                        validationresult.Errors.Add("A role with the same name already exists.");
                    }
                }
                
                if(opcion == 2) // Actualizar
                {
                    if (entity.Id <= 0)
                    {
                        validationresult.Errors.Add("A valid role ID is required for update.");
                    }
                    else
                    {
                        var existingRole = await _rolRepository.GetEntityBy(entity.Id);
                        if (existingRole == null)
                        {
                            validationresult.Errors.Add("The role to be updated does not exist.");
                        }
                    }
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
