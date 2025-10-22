using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Validators.Configuration.BibliotecarioValidators
{
    public class BibliotecarioUpdateValidator : IValidatorBase<BibliotecarioUpdateDto>
    {
        private readonly IBibliotecariosRepository _biblioRepository;
        private readonly ILogger<BibliotecarioCreateValidator> _logger;

        public BibliotecarioUpdateValidator(IBibliotecariosRepository biblioRepository, ILogger<BibliotecarioCreateValidator> logger)
        {
            _biblioRepository = biblioRepository;
            _logger = logger;
        }


        //No se implementa
        public Task<ValidationResult> ValidateCreate(BibliotecarioUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        //("Implementacion requerida")
        public async Task<ValidationResult> ValidateUpdate(BibliotecarioUpdateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields

                //Status is enum, it will always have a value, no need to check for null
                var statusValues = Enum.GetValues(typeof(Status?)).Cast<Status?>();
                if (!statusValues.Contains(entity.BiblioEstatus))
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

                // Check TotalDevoluciones is not negative
                if (entity.TotalDevoluciones < 0)
                {
                    validationResult.AddError("TotalDevoluciones cannot be negative.");
                }

                // Check TotalHorasTrabajadas is not negative
                if (entity.TotalHorasTrabajadas < 0)
                {
                    validationResult.AddError("TotalHorasTrabajadas cannot be negative.");
                }

                // Check TotalClientesAtendidos is not negative
                if (entity.TotalClientesAtendidos < 0)
                {
                    validationResult.AddError("TotalClientesAtendidos cannot be negative.");
                }

                // Check TotalPrestamos is not negative
                if (entity.TotalPrestamos < 0)
                {
                    validationResult.AddError("TotalPrestamos cannot be negative.");
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
    }
}
