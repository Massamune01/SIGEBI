using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class PrestamosServices : IPrestamosService
    {
        private readonly IPrestamosRepository _prestamosRepository;
        private readonly ILogger<PrestamosServices> _logger;
        private readonly IValidatorBase<PrestamoCreateDto> _createValidator;
        private readonly IValidatorBase<PrestamoUpdateDto> _updateValidator;
        private readonly ICacheService _cacheService;

        public PrestamosServices(IPrestamosRepository prestamosRepository, ILogger<PrestamosServices> logger, 
            IValidatorBase<PrestamoCreateDto> createValidator, IValidatorBase<PrestamoUpdateDto> updateValidator
            , ICacheService cacheService)
        {
            _prestamosRepository = prestamosRepository;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreatePrestamoAsync(PrestamoCreateDto prestamoCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Business Validation logic to create a loan
                var prestamoValidation = await _createValidator.ValidateCreate(prestamoCreateDto);
                
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
                if( OpPrestamo is null) 
                { 
                    result.Success = false;
                    result.Message = "Failed to create the loan.";
                    return result;
                }
                result.Success = true;
                result.Data = OpPrestamo;
                result.Message = "Loan created successfully.";
                _cacheService.ClearKeys();
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
                _cacheService.ClearKeys();
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
            const string cacheKey = "ALL_Prestamos";
            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<PrestamoDto> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Prestamos retrieved from cache.";
                return result;
            }

            try
            {
                _logger.LogInformation("Retrieving all loans.");
                var prestamos =  await _prestamosRepository.GetAll();
                if (prestamos.Data is null)
                {
                    result.Success = false;
                    result.Message = "No loans found.";
                    return result;
                }
                result.Success = true;
                result.Data = prestamos.Data;
                result.Message = "Loans retrieved successfully.";
                _cacheService.Set(cacheKey, result.Data);
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
                var prestamoValidation = await _updateValidator.ValidateUpdate(prestamoUpdateDto);

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
                var existingPrestamo = await _prestamosRepository.GetPrestamosById(prestamoUpdateDto.Id);
                if (existingPrestamo is null)
                {
                    result.Success = false;
                    result.Message = "Loan not found.";
                    return result;
                }
                Prestamos prestamo = new Prestamos()
                {
                    Id = prestamoUpdateDto.Id,
                    DateWasDevol = prestamoUpdateDto.DateWasDevol,
                    Status = prestamoUpdateDto.PrestamosStatus,
                    IdLibros = existingPrestamo.IdLibros,
                    IdCliente = existingPrestamo.IdCliente,
                    DatePrest = existingPrestamo.DatePrest,
                    DateDevol = existingPrestamo.DateDevol,
                    IdLgOpPrest = existingPrestamo.IdLgOpPrest
                };
                var updatedPrestamo = await _prestamosRepository.Update(prestamo);
                if (updatedPrestamo is null)
                {
                    result.Success = false;
                    result.Message = "Failed to update the loan.";
                    return result;
                }
                result.Success = true;
                result.Data = updatedPrestamo;
                result.Message = "Loan updated successfully.";
                _cacheService.ClearKeys();
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
