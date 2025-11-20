using SIGEBI.Application.Dtos.Configuration.LibroDtos;

namespace SIGEBI.Web.ViewModels.Libro
{
    public class GetAllLibroResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<LibroDto> Data { get; set; }
    }
}
