using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public sealed class Bibliotecarios : Usuario
    {
        public int TotalDevoluciones { get; set; }
        public int TotalHorasTrabajadas { get; set; }
        public int TotalClientesAtendidos { get; set; }
        public int TotalPrestamos { get; set; }
        public Status BiblioEstatus { get; set; }
        public int? IdLgOpBiblio { get; set; }
    }
}
