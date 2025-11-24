using AutoMapper;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Application.Facades_Classes.Configuration
{

    public class PrestamoFacade : IPrestamoFacade
    {
        private readonly IPrestamosRepository _prestamosRepository;
        private readonly IValidatorBase<PrestamoDto> _Validator;
        private readonly ILogger<PrestamoFacade> _logger;
        private readonly IMapper _mapper;

        public PrestamoFacade(IPrestamosRepository prestamosRepository,
                              IValidatorBase<PrestamoDto> validator,
                              ILogger<PrestamoFacade> logger,
                              IMapper mapper)
        {
            _prestamosRepository = prestamosRepository;
            _Validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreatePrestamoAsync(PrestamoCreateDto prestamoCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business Validation logic to create a loan
                _logger.LogInformation("Validating LogOperations");
                PrestamoDto prestamoDto = new PrestamoDto()
                {
                    DatePrest = prestamoCreateDto.DatePrest,
                    DateDevol = prestamoCreateDto.DateDevol,
                    IdLibros = prestamoCreateDto.IdLibros,
                    IdCliente = prestamoCreateDto.IdCliente,
                    IdLgOpPrest = prestamoCreateDto.IdLgOpLibro
                };

                var prestamoValidation = await _Validator.Validate(prestamoDto, 1);

                if (!prestamoValidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", prestamoValidation.Errors);
                    return result;
                }


                _logger.LogInformation("Creating a loan for client ID: {ClientId}", prestamoCreateDto.IdCliente);
                if (prestamoCreateDto is null)
                {
                    result.Success = false;
                    result.Message = "The loan data cannot be null.";
                    return result;
                }
                Prestamos prestamo = new Prestamos()
                {
                    DatePrest = prestamoCreateDto.DatePrest,
                    DateDevol = prestamoCreateDto.DateDevol,
                    IdLibros = prestamoCreateDto.IdLibros,
                    IdCliente = prestamoCreateDto.IdCliente,
                    IdLgOpPrest = prestamoCreateDto.IdLgOpLibro
                };
                var OpPrestamo = await _prestamosRepository.Save(prestamo);
                if (OpPrestamo is null)
                {
                    result.Success = false;
                    result.Message = "Failed to create the loan.";
                    return result;
                }
                result.Success = true;
                result.Data = OpPrestamo;
                result.Message = "Loan created successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the loan.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> DeletePrestamoAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Deleting loan with ID: {LoanId}", id);
                var existingPrestamo = await _prestamosRepository.GetPrestamosById(id);
                if (existingPrestamo is null)
                {
                    result.Success = false;
                    result.Message = "Loan not found.";
                    return result;
                }
                var deleteResult = await _prestamosRepository.Remove(existingPrestamo);
                if (!deleteResult.Success || deleteResult.Data is null)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the loan.";
                    return result;
                }
                result.Success = true;
                result.Message = "Loan deleted successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the loan.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetAllPrestamosAsync()
        {
            ServiceResult result = new ServiceResult();

            try
            {
                _logger.LogInformation("Retrieving all loans.");
                var prestamos = await _prestamosRepository.GetAll();
                if (!prestamos.Success)
                {
                    result.Success = false;
                    result.Message = "No loans found.";
                    return result;
                }

                List<PrestamoDto> prestamosList = _mapper.Map<List<PrestamoDto>>(prestamos.Data);

                result.Success = true;
                result.Data = prestamosList;
                result.Message = "Loans retrieved successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving loans.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetLibroWithTituloAndISBN()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving Libro with Titulo and ISBN");
                var prestamos = await _prestamosRepository.GetLibroWithTituloAndISBN();
                if (prestamos.Data is null)
                {
                    result.Success = false;
                    result.Message = "No books found.";
                    return result;
                }
                result.Success = true;
                result.Data = prestamos.Data;
                result.Message = "Books retrieved successfully.";
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

        public async Task<ServiceResult> GetPrestamoByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving loan with ID: {LoanId}", id);
                var prestamo = await _prestamosRepository.GetPrestamosById(id);
                if (prestamo is null)
                {
                    result.Success = false;
                    result.Message = "Loan not found.";
                    return result;
                }
                result.Success = true;
                result.Data = prestamo;
                result.Message = "Loan retrieved successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the loan.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdatePrestamoAsync(PrestamoUpdateDto prestamoUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business Validation logic to create a loan
                _logger.LogInformation("Validating LogOperations");
                PrestamoDto prestamoDto = new PrestamoDto()
                {
                    Id = prestamoUpdateDto.Id,
                    DateWasDevol = prestamoUpdateDto.DateWasDevol,
                    Status = prestamoUpdateDto.Status
                };
                var prestamoValidation = await _Validator.Validate(prestamoDto, 2);

                if (!prestamoValidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", prestamoValidation.Errors);
                    return result;
                }

                _logger.LogInformation("Updating loan with ID: {LoanId}", prestamoUpdateDto.Id);
                if (prestamoUpdateDto is null)
                {
                    result.Success = false;
                    result.Message = "The loan data cannot be null.";
                    return result;
                }

                Prestamos prestamo = new Prestamos()
                {
                    Id = prestamoUpdateDto.Id,
                    DateWasDevol = prestamoUpdateDto.DateWasDevol,
                    Status = prestamoUpdateDto.Status,
                    DatePrest = prestamoUpdateDto.DatePrest,
                    DateDevol = prestamoUpdateDto.DateDevol,
                    IdLibros = prestamoUpdateDto.IdLibros,
                    IdCliente = prestamoUpdateDto.IdCliente,
                    IdLgOpPrest = prestamoUpdateDto.IdLgOpPrest

                };
                var updatedPrestamo = await _prestamosRepository.Update(prestamo);
                if (!updatedPrestamo.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to update the loan.";
                    return result;
                }
                result.Success = true;
                result.Data = updatedPrestamo.Data;
                result.Message = "Loan updated successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the loan.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
