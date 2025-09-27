using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Prestamos
{
    public class Prestamos : BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime DatePrest { get; set; }
        public DateTime DateDevol {  get; set; }
        public DateTime? DateWasDevol { get; set; }
        public Status PrestamosStatus { get; set; } = Status.Activo;
    }
}
