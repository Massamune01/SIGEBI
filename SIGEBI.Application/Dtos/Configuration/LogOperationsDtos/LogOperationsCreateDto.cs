using System;
using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    public record CreateLogOperationDto
    {
        [StringLength(90, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 90")]
        public string Entity { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [StringLength(90, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 90")]
        public string TypeOperation { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 100")]
        public string? Descripcion { get; set; }
        public Status StatusOp { get; set; } = Status.Activo;
    }
}
