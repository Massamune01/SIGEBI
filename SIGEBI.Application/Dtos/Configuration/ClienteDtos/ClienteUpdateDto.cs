using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.ClienteDtos
{
    public record ClienteUpdateDto : UsuarioUpdateDto
    {
        public int CapacidadPrest { get; set; }
        public int TotalDevolRestrasadas { get; set; }
        public int PrestamosActivos { get; set; }
        public int TotalDevoluciones { get; set; } = 0;
        public int TotalPrestamos { get; set; } = 0;
        public Status StatusCliente { get; set; }
    }
}
