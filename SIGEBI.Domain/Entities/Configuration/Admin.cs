using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public sealed class Admin : Usuario 
    {
        public Status AdminEstatus { get; set; } = Status.Activo;
        public int? IdLgOpAdmin { get; set; }
    }
}
