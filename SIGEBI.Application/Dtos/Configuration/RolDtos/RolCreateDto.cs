using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolCreateDto
    {
        [Required, StringLength(60)]
        public string Nombre { get; set; } = null!;
        public int? IdLgOpRol { get; set; }
    }
}
