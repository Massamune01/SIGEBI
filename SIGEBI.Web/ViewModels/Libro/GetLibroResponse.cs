using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;

namespace SIGEBI.Web.ViewModels.Libro
{
    public class GetLibroResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LibroDto Data { get; set; }
    }
}
