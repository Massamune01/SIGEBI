using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public sealed class Roles
    {
        public int Id { get; set; }
        public string Rol { get; set; }

        public Status RolEstatus { get; set; } = Status.Activo;
        public int? IdLgOpRol { get; set; }

    }
}
