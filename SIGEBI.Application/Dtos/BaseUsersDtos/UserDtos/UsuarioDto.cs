using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.BaseDtos.UserDtos
{
    public record UsuarioDto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public string? Email { get; set; }
        public DateOnly? Nacimiento { get; set; }
        public int? RolId { get; set; }
    }
}
