using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    public record CreateLogOperationDto
    {
        public string Entity { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string TypeOperation { get; set; } = null!;
        public string? Descripcion { get; set; }
        public Status StatusOp { get; set; } = Status.Activo;
    }
}
