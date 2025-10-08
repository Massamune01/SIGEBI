using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;


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
                var statusValues = Enum.GetValues(typeof(Domain.Enums.Status?)).Cast<Domain.Enums.Status?>();
                if (!statusValues.Contains(entity.AdminEstatus))
                {
                    validationResult.AddError("Invalid status value.");
                }

                // Example validation: Check if Nombre is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Nombre))
                {
                    validationResult.AddError("Nombre is required.");
                }

                // Example validation: Check if email is already in use
                var existingAdmin =  _adminRepository.GetAdminByEmail(entity.Email).Result;
                if (existingAdmin != null)
                {
                    validationResult.AddError("Email is already in use.");
                }
                // Add more validation rules as needed

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
