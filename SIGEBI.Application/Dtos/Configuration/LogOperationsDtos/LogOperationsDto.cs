using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    internal record LogOperationsDto
    {
        [Key]
        public int IdOp { get; set; }
        public string Entity { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string TypeOperation { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? LastUpdateBy { get; set; }
        public DateTime? UpdateBy { get; set; }
        public Status StatusOp { get; set; } = Status.Activo;
    }
}
