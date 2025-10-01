using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.ClienteDtos
{
    public record ClienteCreateDto : UsuarioCreateDto
    {
        public int? CapacidadPrest { get; set; }
        public Status? StatusCliente { get; set; } = Status.Activo;
        public int? TotalDevoluciones { get; set; } = 0;
        public int? TotalDevolRestrasadas { get; set; } = 0;
        public int? TotalPrestamos { get; set; } = 0;
        public int? PrestamosActivos { get; set; } = 0;

    }
}
