using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Persistence.Models.Configuration.Rol
{
    public record RolGetModel
    {
        [Key]
        public int Id { get; set; }
        public string Rol { get; set; } = null!;
        public Status RolEstatus { get; set; }
        public int? IdLgOpRol { get; set; }
    }
}
