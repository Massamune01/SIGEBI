using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.AdminDtos
{
    public record AdminUpdateDto : UsuarioUpdateDto
    {
        public Status AdminEstatus { get; set; }
        public int? IdLgOpAdmin { get; set; }
    }
}
