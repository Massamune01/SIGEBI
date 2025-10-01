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
        List<Bibliotecarios> GetBiblioById(int id);
        List<Bibliotecarios> GetBiblioByName(string name);
        List<Bibliotecarios> GetBiblioByRol(int rol);
        List<Bibliotecarios> GetAdminByStatus(Status status);
    }
}
