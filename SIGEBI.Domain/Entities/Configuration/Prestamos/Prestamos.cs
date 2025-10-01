using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration.Prestamos
{
    public class Prestamos
    {
        public int Id { get; set; }
        public DateTime DatePrest { get; set; }
        public DateTime DateDevol {  get; set; }
        public DateTime? DateWasDevol { get; set; }
        public Status PrestamosStatus { get; set; } = Status.Activo;
        public int IdLibros { get; set; }
        public int IdCliente { get; set; }
        public int? IdLgOpPrest { get; set; }
    }
}
