using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;

namespace SIGEBI.Web.ViewModels.Prestamo
{
    public class GetAllPrestamoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<PrestamoDto> Data { get; set; }
    }
}
