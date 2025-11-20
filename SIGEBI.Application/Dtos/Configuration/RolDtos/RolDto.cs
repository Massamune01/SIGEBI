using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 30")]
        public string? Rol { get; set; }
        public Status RolEstatus { get; set; } = Status.Activo;
        public int? IdLgOpRol { get; set; }
    }
}
