using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    public class UpdateLogOperationDto
    {
        public int IdOp { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? UpdateBy { get; set; }
        public Status StatusOp { get; set; } = Status.Activo;
        public DateTime? LastUpdateBy { get; set; }
        
    }
}
