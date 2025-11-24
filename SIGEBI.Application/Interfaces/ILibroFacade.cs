using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface ILibroFacade
    {
        Task<ServiceResult> GetAllLibrosAsync();
        Task<ServiceResult> GetLibroByIdAsync(Int64 id);
        Task<ServiceResult> CreateLibroAsync(LibroCreateDto libroCreateDto);
        Task<ServiceResult> UpdateLibroAsync(LibroUpdateDto libroUpdateDto);
        Task<ServiceResult> DeleteLibroAsync(Int64 id);
    }
}
