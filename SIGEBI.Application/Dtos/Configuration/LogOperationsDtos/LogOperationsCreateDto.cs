using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    public record CreateLogOperationDto
    {
        public string Entity { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string TypeOperation { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string StatusOp { get; set; } = null!;
    }
}
