using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Validators.Configuration.BibliotecarioValidators
{
    public class BibliotecarioValidator : IValidatorBase<BibliotecarioDto>
    {
        private readonly IBibliotecariosRepository _biblioRepository;
        private readonly ILogger<BibliotecarioValidator> _logger;

        public BibliotecarioValidator(IBibliotecariosRepository biblioRepository, ILogger<BibliotecarioValidator> logger)
        {
            _biblioRepository = biblioRepository;
            _logger = logger;
        }


        //[("Unica implementacion")]
        public async Task<ValidationResult> Validate(BibliotecarioDto entity,int opcion)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                // Basic validation: Check for required fields
                if (opcion == 1) // Create
                {
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
                else if(opcion == 2) // Update
                {
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
                    // TotalDevoluciones cannot be negative
                    if(entity.TotalDevoluciones < 0)
                    {
                        validationResult.AddError("TotalDevoluciones cannot be negative.");
                    }

                    // TotalPrestamos cannot be negative
                    if (entity.TotalPrestamos < 0)
                    {
                        validationResult.AddError("TotalPrestamos cannot be negative.");
                    }

                    if(entity.TotalPrestamos < entity.TotalDevoluciones)
                    {
                        validationResult.AddError("TotalDevoluciones cannot be greater than TotalPrestamos.");
                    }

                    if(entity.TotalClientesAtendidos < 0) 
                    {
                        validationResult.AddError("TotalClientesAtendidos cannot be negative.");
                    }

                    if(entity.TotalHorasTrabajadas < 0) 
                    {
                        validationResult.AddError("TotalHorasTrabajadas cannot be negative.");
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
