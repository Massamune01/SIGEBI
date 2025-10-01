using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.AdminDtos
{
    public record AdminCreateDto : UsuarioCreateDto
    {
        public Status? AdminEstatus { get; set; } = Status.Activo;
        public int? IdLgOpAdmin { get; set; }

    }
}
