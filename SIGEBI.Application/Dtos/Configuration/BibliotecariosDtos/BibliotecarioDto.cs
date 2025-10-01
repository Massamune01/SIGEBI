using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos
{
    public record BibliotecarioDto : UsuarioDto
    {
        public int TotalDevoluciones { get; set; }
        public int TotalHorasTrabajadas { get; set; }
        public int TotalClientesAtendidos { get; set; }
        public int TotalPrestamos { get; set; }
        public Status? BiblioEstatus { get; set; }

    }
}
