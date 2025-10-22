using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Enums;


namespace SIGEBI.Application.Validators.Configuration.AdminValidators
{
    public class AdminCreateValidation : IValidatorBase<AdminCreateDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<AdminCreateValidation> _logger;

        public AdminCreateValidation(IAdminRepository adminRepository, ILogger<AdminCreateValidation> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> ValidateCreate(AdminCreateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields

                //Status is enum, it will always have a value, no need to check for null
                var statusValues = Enum.GetValues(typeof(Status?)).Cast<Status?>();
                if (!statusValues.Contains(entity.AdminEstatus))
                {
                    validationResult.AddError("Invalid status value.");
                }

                // Check if Nombre is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Nombre))
                {
                    validationResult.AddError("Nombre is required.");
                }


                // Check if Apellido is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Apellido))
                {
                    validationResult.AddError("Apellido is required.");
                }

                //Edad should be greater than 17
                if (entity.Edad < 17)
                {
                    validationResult.AddError("Edad must be at least 17.");
                }

                // Nacimiento should not be in the future
                if (entity.Nacimiento.HasValue && entity.Nacimiento > DateOnly.FromDateTime(DateTime.Now))
                {
                    validationResult.AddError("Nacimiento cannot be in the future.");
                }

                // Nacimiento cannot be same as today
                if (entity.Nacimiento.HasValue && entity.Nacimiento == DateOnly.FromDateTime(DateTime.Now))
                {
                    validationResult.AddError("Nacimiento cannot be today's date.");
                }


                // Check if Cedula is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Cedula))
                {
                    validationResult.AddError("Cedula is required.");
                }

                //Check if Cedula is already in use
                var existingCedula = _adminRepository.GetAdminByCedulaAsync(entity.Cedula).Result;
                if(existingCedula != null)
                {
                    validationResult.AddError("Cedula is already in use.");
                }

                // Check if Email is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Email))
                {
                    validationResult.AddError("Email is required.");
                }

                // Check if email is already in use
                var existingEmail =  _adminRepository.GetAdminByEmailAsync(entity.Email).Result;
                if (existingEmail != null)
                {
                    validationResult.AddError("Email is already in use.");
                }


                
                return validationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during admin validation.");
                validationResult.AddError("An error occurred during validation.");
                return validationResult;
            }

        }
        public Task<ValidationResult> ValidateUpdate(AdminCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
