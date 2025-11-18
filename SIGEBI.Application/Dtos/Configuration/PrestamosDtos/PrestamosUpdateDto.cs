using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.PrestamosDtos
{
    public class PrestamoUpdateDto
    {
        public int Id { get; set; }
        public DateTime DatePrest { get; set; }
        public DateTime DateDevol { get; set; } 
        public DateTime? DateWasDevol { get; set; }
        public Status Status { get; set; }
        public Int64 IdLibros { get; set; }
        public int IdCliente { get; set; }
    }
}
