using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Application.Validators.Configuration.PrestamosValidators
{
    public class PrestamoValidator : IValidatorBase<PrestamoDto>
    {
        private readonly IPrestamosRepository _prestamosRepository;
        private readonly ILogger<PrestamoValidator> _logger;

        public PrestamoValidator(IPrestamosRepository prestamosRepository, ILogger<PrestamoValidator> logger)
        {
            _prestamosRepository = prestamosRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Validate(PrestamoDto entity, int opcion)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                if(opcion == 1) // Create operation
                {
                    _logger.LogInformation("Validating PrestamoCreateDto for client ID: {ClientId}", entity.IdCliente);
                    // Basic validation: Check for required fields
                    if (entity.DatePrest == default)
                    {
                        validationResult.AddError("DatePrest is required.");
                    }

                    // Check if the book is available for loan
                    var prestamosExistentes = await _prestamosRepository.GetLibroForPrestamoAsync(entity.IdLibros);
                    if (!prestamosExistentes.Success)
                    {
                        validationResult.AddError(prestamosExistentes.Message);
                    }
                    return validationResult;

                }
                else if(opcion == 2) // Update operation
                {
                    // Implement update validation logic if needed
                    if (entity.Id <= 0)
                    {
                        validationResult.AddError("Invalid Prestamo ID for update.");
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
                _logger.LogError(ex, "An error occurred during Prestamo validation.");
                validationResult.AddError("An unexpected error occurred during validation.");
                return validationResult;
            }
        }
    }
}
