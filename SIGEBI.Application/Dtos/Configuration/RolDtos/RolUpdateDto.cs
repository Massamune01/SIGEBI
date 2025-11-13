using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolUpdateDto
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Rol { get; set; } = null!;
    }
}
