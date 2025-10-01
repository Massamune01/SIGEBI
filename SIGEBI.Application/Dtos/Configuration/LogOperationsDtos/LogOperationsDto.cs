using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    internal record LogOperationsDto
    {
        public int IdOp { get; set; }
        public string Entity { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string TypeOperation { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? LastUpdateBy { get; set; }
        public DateTime? UpdateBy { get; set; }
        public string StatusOp { get; set; } = null!;
    }
}
