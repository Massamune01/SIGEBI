using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface IPrestamoFacade
    {
        Task<ServiceResult> GetAllPrestamosAsync();
        Task<ServiceResult> GetPrestamoByIdAsync(int id);
        Task<ServiceResult> CreatePrestamoAsync(PrestamoCreateDto prestamoCreateDto);
        Task<ServiceResult> UpdatePrestamoAsync(PrestamoUpdateDto prestamoUpdateDto);
        Task<ServiceResult> DeletePrestamoAsync(int id);
        Task<ServiceResult> GetLibroWithTituloAndISBN();
    }
}
