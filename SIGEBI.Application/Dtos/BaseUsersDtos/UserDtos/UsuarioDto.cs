using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.BaseDtos.UserDtos
{
    public record UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Edad { get; set; }
        public Genero Genero { get; set; }         
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }

        public Status UserEstatus { get; set; }
        public int RolId { get; set; }
        public string? RolNombre { get; set; }
        public virtual int? IdLgOpLibro { get; set; }
    }
}
