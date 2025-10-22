using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface IPrestamosRepository : IBaseRepository<Prestamos>
    {
        Task<Prestamos?> GetPrestamosById(int id);
        Task<List<Prestamos>> GetPrestamosByClienteId(int clienteId);
        Task<OperationResult> GetLibroForPrestamoAsync(int IdLibro);
    }
}
