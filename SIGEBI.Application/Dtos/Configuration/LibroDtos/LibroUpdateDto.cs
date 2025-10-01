﻿using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LibroDtos
{
    public class LibroUpdateDto
    {
        [Required]
        public int ISBN { get; set; }

        public string? titulo { get; set; }
        public string? autor { get; set; }
        public string? editorial { get; set; }
        public int? anioPublicacion { get; set; }
        public string? categoria { get; set; }
        public int? numPaginas { get; set; }
        public int? cantidad { get; set; }
        public bool? disponible { get; set; }
        public Status Status { get; set; } = Status.Activo;
    }
}
