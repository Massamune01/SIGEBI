using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Validators.Configuration.BibliotecarioValidators
{
    public class BibliotecarioCreateValidator : IValidatorBase<BibliotecarioCreateDto>
    {
        private readonly IBibliotecariosRepository _biblioRepository;
        private readonly ILogger<BibliotecarioCreateValidator> _logger;

        public BibliotecarioCreateValidator(IBibliotecariosRepository biblioRepository, ILogger<BibliotecarioCreateValidator> logger)
        {
            _biblioRepository = biblioRepository;
            _logger = logger;
        }


        //[("Unica implementacion")]
        public async Task<ValidationResult> ValidateCreate(BibliotecarioCreateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields

                //Check if Cedula is already in use
                var existingCedula = _biblioRepository.GetBiblioByCedula(entity.Cedula).Result;
                if (existingCedula != null)
                {
                    validationResult.AddError("Cedula is already in use.");
                }

                // Check if Email is not null or empty
                if (string.IsNullOrWhiteSpace(entity.Email))
                {
                    validationResult.AddError("Email is required.");
                }

                // Check if email is already in use
                var existingEmail = _biblioRepository.GetBiblioByEmail(entity.Email).Result;
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


        //No se implementa
        public Task<ValidationResult> ValidateUpdate(BibliotecarioCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
