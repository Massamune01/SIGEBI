using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Application.Validators.Configuration.PrestamosValidators
{
    public class PrestamoCreateValidator : IValidatorBase<PrestamoCreateDto>
    {
        private readonly IPrestamosRepository _prestamosRepository;
        private readonly ILogger<PrestamoCreateValidator> _logger;

        public PrestamoCreateValidator(IPrestamosRepository prestamosRepository, ILogger<PrestamoCreateValidator> logger)
        {
            _prestamosRepository = prestamosRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> ValidateCreate(PrestamoCreateDto entity)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                _logger.LogInformation("Validating PrestamoCreateDto for client ID: {ClientId}", entity.IdCliente);
                // Basic validation: Check for required fields
                if (entity.DatePrest == default)
                {
                    validationResult.AddError("DatePrest is required.");
                }
                //Check ISBN max 13 digit and min 13 digit
                if (entity.IdLibros.ToString().Length != 13)
                {
                    validationResult.AddError("IdLibros must be exactly 13 digits long.");
                }
                // Check for required fields
                if (entity.DateDevol == default)
                {
                    validationResult.AddError("DateDevol is required.");
                }
                // Check for positive integers
                if (entity.IdLibros <= 0)
                {
                    validationResult.AddError("IdLibros must be a positive integer.");
                }
                if (entity.IdCliente <= 0)
                {
                    validationResult.AddError("IdCliente must be a positive integer.");
                }
                if (entity.IdLgOpLibro <= 0)
                {
                    validationResult.AddError("IdLgOpLibro must be a positive integer.");
                }

                // Check that DateDevol is after DatePrest
                if (entity.DateDevol <= entity.DatePrest)
                {
                    validationResult.AddError("DateDevol must be after DatePrest.");
                }

                // Check if the book is available for loan
                var prestamosExistentes = await _prestamosRepository.GetLibroForPrestamoAsync(entity.IdLibros);
                if (!prestamosExistentes.Success)
                {
                    validationResult.AddError(prestamosExistentes.Message);
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during PrestamoCreateDto validation.");
                validationResult.AddError("An unexpected error occurred during validation.");
                return validationResult;
            }
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(PrestamoCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
