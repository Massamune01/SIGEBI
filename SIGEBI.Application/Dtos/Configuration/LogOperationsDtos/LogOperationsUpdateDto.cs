namespace SIGEBI.Application.Dtos.Configuration.LogOperationsDtos
{
    public class UpdateLogOperationDto
    {
        public int IdOp { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? UpdateBy { get; set; }
        public string? StatusOp { get; set; }
        public DateTime? LastUpdateBy { get; set; }
        
    }
}
