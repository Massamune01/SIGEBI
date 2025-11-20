using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolCreateDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 30")]
        public string Rol { get; set; } = null!;
        public int? IdLgOpRol { get; set; }
    }
}
