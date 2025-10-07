using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.RolDtos
{
    public record RolRemoveDto
    {
        public int Id { get; set; }
        public Status RolEstatus { get; set; }
        public int? IdLgOpLibro { get; set; }
    }
}
