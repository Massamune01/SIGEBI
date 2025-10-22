using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.PrestamosDtos
{
    public class PrestamoUpdateDto
    {
        public int Id { get; set; }
        public DateTime? DateWasDevol { get; set; }
        public Status PrestamosStatus { get; set; }
    }
}
