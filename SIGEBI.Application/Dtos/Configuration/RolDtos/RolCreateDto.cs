using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolCreateDto
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Rol { get; set; } = null!;
        public int? IdLgOpRol { get; set; }
    }
}
