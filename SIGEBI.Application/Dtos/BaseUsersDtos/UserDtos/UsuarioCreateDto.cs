using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.BaseDtos.UserDtos
{
    public record UsuarioCreateDto
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public int Edad { get; set; }

        public Genero Genero { get; set; }     

        [EmailAddress]
        public string? Email { get; set; }

        public DateOnly? Nacimiento { get; set; }
        public int RolId { get; set; }
    }

}
