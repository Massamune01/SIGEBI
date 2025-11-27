using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Enums;


namespace SIGEBI.Application.Validators.Configuration.AdminValidators
{
    public class AdminValidator : IValidatorBase<AdminDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<AdminValidator> _logger;

        public AdminValidator(IAdminRepository adminRepository, ILogger<AdminValidator> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Validate(AdminDto entity, int opcion)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields
                if (opcion == 1) // Create operation
                {

                    // Check if Nombre is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.Nombre))
                    {
                        validationResult.AddError("Nombre is required.");
                        return validationResult;
                    }


                    // Check if Apellido is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.Apellido))
                    {
                        validationResult.AddError("Apellido is required.");
                        return validationResult;
                    }

                    //Edad should be greater than 17
                    if (entity.Edad < 17)
                    {
                        validationResult.AddError("Edad must be at least 17.");
                        return validationResult;
                    }

                    // Nacimiento should not be in the future
                    if (entity.Nacimiento.HasValue && entity.Nacimiento > DateOnly.FromDateTime(DateTime.Now))
                    {
                        validationResult.AddError("Nacimiento cannot be in the future.");
                        return validationResult;
                    }

                    // Nacimiento cannot be same as today
                    if (entity.Nacimiento.HasValue && entity.Nacimiento == DateOnly.FromDateTime(DateTime.Now))
                    {
                        validationResult.AddError("Nacimiento cannot be today's date.");
                        return validationResult;
                    }


                    // Check if Cedula is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.Cedula))
                    {
                        validationResult.AddError("Cedula is required.");
                        return validationResult;
                    }

                    if(entity.Cedula.Length <11 || entity.Cedula.Length > 11)
                    {
                        validationResult.AddError("Cedula needs to be 11 digits");
                        return validationResult;
                    }

                    //Check if Cedula is already in use
                    var cedulaEncontrado = (await _adminRepository.GetAdminByCedulaAsync(entity.Cedula))
                        .FirstOrDefault();

                    if (cedulaEncontrado != null)
                    {
                        validationResult.AddError("Cedula is already in use.");
                        return validationResult;
                    }

                    // Check if Email is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.Email))
                    {
                        validationResult.AddError("Email is required.");
                        return validationResult;
                    }

                    // Check if email is already in use
                    var existingEmail =( await _adminRepository.GetAdminByEmailAsync(entity.Email))
                        .FirstOrDefault();
                    if(existingEmail != null)
                    {
                        validationResult.AddError("Email is already in use.");
                        return validationResult;
                    }

                    return validationResult;
                }
                else if (opcion == 2) // Update operation
                {
                    //Edad should be greater than 17
                    if (entity.Edad < 17)
                    {
                        validationResult.AddError("Edad must be at least 17.");
                        return validationResult;
                    }

                    // Nacimiento should not be in the future
                    if (entity.Nacimiento.HasValue && entity.Nacimiento > DateOnly.FromDateTime(DateTime.Now))
                    {
                        validationResult.AddError("Nacimiento cannot be in the future.");
                        return validationResult;
                    }

                    // Nacimiento cannot be same as today
                    if (entity.Nacimiento.HasValue && entity.Nacimiento == DateOnly.FromDateTime(DateTime.Now))
                    {
                        validationResult.AddError("Nacimiento cannot be today's date.");
                        return validationResult;
                    }


                    // Check if Cedula is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.Cedula))
                    {
                        validationResult.AddError("Cedula is required.");
                        return validationResult;
                    }

                    // Check if Email is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.Email))
                    {
                        validationResult.AddError("Email is required.");
                        return validationResult;
                    }

                    return validationResult;
                }
                else
                {
                    validationResult.AddError("Invalid operation option.");
                    return validationResult;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during admin validation.");
                validationResult.AddError("An error occurred during validation.");
                return validationResult;
            }
        }
    }
}
