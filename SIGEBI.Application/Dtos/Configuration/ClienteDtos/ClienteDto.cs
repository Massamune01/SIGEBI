using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.ClienteDtos
{
    public record ClienteDto : UsuarioDto
    {
        public int TotalDevoluciones { get; set; }
        public int CapacidadPrest { get; set; }
        public int TotalDevolRestrasadas { get; set; }
        public int TotalPrestamos { get; set; }
        public int PrestamosActivos { get; set; }
        public Status? StatusCliente { get; set; }

    }
}
