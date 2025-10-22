using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Repositories.Configuration
{
    public interface IBibliotecariosRepository : IBaseRepository<Bibliotecarios>
    {
        Task<List<Bibliotecarios>> GetBiblioById(int id);
        Task<List<Bibliotecarios>> GetBiblioByName(string name);
        Task<List<Bibliotecarios>> GetBiblioByCedula(string cedula);
        Task<List<Bibliotecarios>> GetBiblioByEmail(string email);
        Task<List<Bibliotecarios>> GetBiblioByRol(int rol);
        Task<List<Bibliotecarios>> GetBiblioByStatus(Status status);
    }
}
