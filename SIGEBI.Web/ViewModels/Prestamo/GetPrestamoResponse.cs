using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;

namespace SIGEBI.Web.ViewModels.Prestamo
{
    public class GetPrestamoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public PrestamoDto Data { get; set; }
    }
}
