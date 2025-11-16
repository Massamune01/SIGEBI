using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
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
