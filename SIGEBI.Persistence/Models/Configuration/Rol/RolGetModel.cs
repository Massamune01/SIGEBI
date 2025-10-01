using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Persistence.Models.Configuration.Rol
{
    public record RolGetModel
    {
        public int Id { get; set; }
        public string Rol { get; set; } = null!;
        public Status RolEstatus { get; set; }
        public int? IdLgOpLibro { get; set; }
    }
}
