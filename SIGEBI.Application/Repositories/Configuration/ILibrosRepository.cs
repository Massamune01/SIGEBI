using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface ILibrosRepository : IBaseRepository<Libro>
    {
        Task<Libro?> GetLibroById(Int64 id);
        Task<List<Libro>> GetLibroByTitulo(string titulo);
        Task<List<Libro>> GetLibroByAutor(string autor);
        Task<List<Libro>> GetLibroByEditorial(string editorial);
        Task<List<Libro>> GetLibroByCategoria(string categoria);
    }
}
