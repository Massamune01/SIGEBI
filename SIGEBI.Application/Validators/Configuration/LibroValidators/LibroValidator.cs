using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;

namespace SIGEBI.Application.Validators.Configuration.LibroValidators
{
    public class LibroValidator : IValidatorBase<LibroDto>
    {
        private readonly ILibrosRepository _libroRepository;
        private readonly ILogger<LibroValidator> _logger;

        public LibroValidator(ILibrosRepository libroRepository, ILogger<LibroValidator> logger)
        {
            _libroRepository = libroRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Validate(LibroDto entity, int opcion)
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
               if (opcion == 1) // Create operation
                {
                    _logger.LogInformation("Validating Libro creation for ISBN: {ISBN}", entity.ISBN);

                    // Basic validation: Check for required fields
                    // Check if ISBN is not null or empty
                    if (entity.ISBN == null || entity.ISBN <= 0)
                    {
                        validationResult.AddError("ISBN is required.");
                        return validationResult;
                    }
                    // Check if titulo is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.titulo))
                    {
                        validationResult.AddError("Titulo is required.");
                        return validationResult;
                    }
                    // Check if autor is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.autor))
                    {
                        validationResult.AddError("Autor is required.");
                        return validationResult;
                    }
                    // Check for unique ISBN
                    var existingLibros = await _libroRepository.GetLibroById(entity.ISBN);
                    if (existingLibros != null)
                    {
                        validationResult.AddError("A libro with the same ISBN already exists.");
                        return validationResult;
                    }

                    // Check if cantidad is non-negative
                    if (entity.cantidad <= 0)
                    {
                        validationResult.AddError("Cantidad cannot be negative.");
                        return validationResult;
                    }

                    return validationResult;
                }
                else if (opcion == 2) // Update operation
                {
                    // Check if titulo is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.titulo))
                    {
                        validationResult.AddError("Titulo is required.");
                        return validationResult;
                    }
                    // Check if autor is not null or empty
                    if (string.IsNullOrWhiteSpace(entity.autor))
                    {
                        validationResult.AddError("Autor is required.");
                        return validationResult;
                    }
                    // Check if cantidad is non-negative
                    if (entity.numPaginas <= 0)
                    {
                        validationResult.AddError("Cantidad cannot be negative.");
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
                _logger.LogError(ex, "Error during Libro creation validation.");
                validationResult.AddError("An error occurred during validation. Please try again later.");
                return validationResult;
            }

        }
    }
}
