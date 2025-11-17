using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibrosRepository _libroRepository;
        private readonly ILogger<LibroService> _logger;
        private readonly IValidatorBase<LibroDto> _Validator;
        private readonly ICacheService _cacheService;
        public LibroService(ILibrosRepository libroRepository, ILogger<LibroService> logger, 
            IValidatorBase<LibroDto> validator,ICacheService cacheService)
        {
            _libroRepository = libroRepository;
            _logger = logger;
            _Validator = validator;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreateLibroAsync(LibroCreateDto libroCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Validating book creation for ISBN: {ISBN}", libroCreateDto.ISBN);
                //Business validations

                LibroDto libroDto = new LibroDto()
                {
                    ISBN = libroCreateDto.ISBN,
                    titulo = libroCreateDto.titulo,
                    autor = libroCreateDto.autor,
                    editorial = libroCreateDto.editorial,
                    anoPublicacion = libroCreateDto.anioPublicacion,
                    categoria = libroCreateDto.categoria,
                    numPaginas = libroCreateDto.numPaginas,
                    cantidad = libroCreateDto.cantidad,
                    IdLgOpLibro = libroCreateDto.IdLgOpLibro,
                };

                var createValidator = await _Validator.Validate(libroDto,1);
                if (!createValidator.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", createValidator.Errors);
                    return result;
                }

                _logger.LogInformation("Creating a book with title: {BookTitle}", libroCreateDto.titulo);

                if (libroCreateDto is null)
                {
                    result.Success = false;
                    result.Message = "The book data cannot be null.";
                    _logger.LogInformation(result.Message);
                    return result;
                }
                // Map LibroCreateDto to Libro entity
                var libro = new Libro()
                {
                    ISBN = libroCreateDto.ISBN,
                    titulo = libroCreateDto.titulo,
                    autor = libroCreateDto.autor,
                    editorial = libroCreateDto.editorial,
                    anoPublicacion = libroCreateDto.anioPublicacion,
                    categoria = libroCreateDto.categoria,
                    numPaginas = libroCreateDto.numPaginas,
                    cantidad = libroCreateDto.cantidad,
                    IdLgOpLibro = libroCreateDto.IdLgOpLibro,
                    Status = libroCreateDto.Status
                };
                var createdLibro = _libroRepository.Save(libro);
                if (createdLibro is null)
                {
                    result.Success = false;
                    result.Message = "Failed to create the book.";
                    return result;
                }
                result.Success = true;
                result.Data = createdLibro;
                result.Message = "Book created successfully.";
                _cacheService.ClearKeys();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the book.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> DeleteLibroAsync(Int64 id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Deleting a book with ID: {BookId}", id);
                var libroToDelete = await _libroRepository.GetLibroById(id);
                if (libroToDelete is null)
                {
                    result.Success = false;
                    result.Message = "Book not found.";
                    return result;
                }
                var deleteResult = await _libroRepository.Remove(libroToDelete);
                if (!deleteResult.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the book.";
                    return result;
                }
                result.Success = true;
                result.Message = "Book deleted successfully.";
                _cacheService.ClearKeys();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the book.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetAllLibrosAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Libros";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<Libro> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Libros retrieved from cache.";
                return result;
            }

            try
            {
                _logger.LogInformation("Retrieving all books.");
                var libros = await _libroRepository.GetAll();
                if (libros is null)
                {
                    result.Success = false;
                    result.Message = "No books found.";
                    return result;
                }

                List<Libro> libroList = libros.Data;


                result.Success = true;
                result.Data = libroList;
                result.Message = "Books retrieved successfully.";
                _cacheService.Set(cacheKey, libroList);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving books.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetLibroByIdAsync(Int64 id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving book with ID: {BookId}", id);
                var libro = await _libroRepository.GetLibroById(id);
                if (libro is null)
                {
                    result.Success = false;
                    result.Message = "Book not found.";
                    return result;
                }
                result.Success = true;
                result.Data = libro;
                result.Message = "Book retrieved successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the book.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdateLibroAsync(LibroUpdateDto libroUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business validations
                _logger.LogInformation("Validating Libro");
                LibroDto libroDto = new LibroDto()
                {
                    ISBN = libroUpdateDto.ISBN,
                    titulo = libroUpdateDto.titulo,
                    autor = libroUpdateDto.autor,
                    editorial = libroUpdateDto.editorial,
                    anoPublicacion = libroUpdateDto.anoPublicacion,
                    categoria = libroUpdateDto.categoria,
                    numPaginas = libroUpdateDto.numPaginas,
                    cantidad = libroUpdateDto.cantidad,
                    IdLgOpLibro = libroUpdateDto.IdLgOpLibro,
                };

                var updateValidator = await _Validator.Validate(libroDto,2);
                if (!updateValidator.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", updateValidator.Errors);
                    return result;
                }

                _logger.LogInformation("Updating book with ID: {BookId}", libroUpdateDto.ISBN);
                if (libroUpdateDto is null)
                {
                    result.Success = false;
                    result.Message = "The book data cannot be null.";
                    return result;
                }
                Libro libro = new Libro()
                {
                    ISBN = libroUpdateDto.ISBN,
                    titulo = libroUpdateDto.titulo,
                    autor = libroUpdateDto.autor,
                    editorial = libroUpdateDto.editorial,
                    anoPublicacion = libroUpdateDto.anoPublicacion,
                    categoria = libroUpdateDto.categoria,
                    numPaginas = libroUpdateDto.numPaginas,
                    cantidad = libroUpdateDto.cantidad,
                    IdLgOpLibro = libroUpdateDto.IdLgOpLibro,
                    Status = libroUpdateDto.Status
                };

                var updateResult = await _libroRepository.Update(libro);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = updateResult.Message;
                    return result;
                }
                result.Success = true;
                result.Message = "Book updated successfully.";
                result.Data = updateResult.Data;
                _logger.LogInformation(result.Message);
                _cacheService.ClearKeys();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the book.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
