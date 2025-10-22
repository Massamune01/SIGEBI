using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<List<Cliente>> GetClienteByIdAsync(int id);
        Task<List<Cliente>> GetClienteByNameAsync(string name);
        Task<List<Cliente>> GetClienteByCedulaAsync(string cedula);
        Task<List<Cliente>> GetClienteByEmail(string email);
    }
}