using System.Reflection.Metadata;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Base
{
    public class LogOperations
    {
        public int IdOp { get; set; }
        public string Entity { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string TypeOperation { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? LastUpdateBy { get; set; }
        public DateTime? UpdateBy { get; set; }
        public string StatusOp { get; set; } = string.Empty;
    }
}
