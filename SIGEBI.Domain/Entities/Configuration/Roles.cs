using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public sealed class Roles
    {
        [Key]
        public int Id { get; set; }
        public string? Rol { get; set; }

        public Status RolEstatus { get; set; } = Status.Activo;
        public int? IdLgOpRol { get; set; }

    }
}
