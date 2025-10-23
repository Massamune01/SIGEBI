using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolDto
    {
        [Key]
        public int Id { get; set; }
        public string? Rol { get; set; }
        public Status RolEstatus { get; set; } = Status.Activo;
        public int? IdLgOpRol { get; set; }
    }
}
