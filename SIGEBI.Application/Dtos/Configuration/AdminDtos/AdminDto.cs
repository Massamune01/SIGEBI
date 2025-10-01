using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.AdminDtos
{
    public record AdminDto : UsuarioDto
    {
        public Status AdminEstatus { get; set; }
        public int? IdLgOpAdmin { get; set; }

    }
}
