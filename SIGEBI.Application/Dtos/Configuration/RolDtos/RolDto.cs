using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolDto
    {
        public int Id { get; set; }
        public string Rol { get; set; } = null!;
        public Status RolEstatus { get; set; }
        public int? IdLgOpLibro { get; set; }
    }
}
