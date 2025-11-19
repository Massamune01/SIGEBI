using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;

namespace SIGEBI.Web.ViewModels.Biblio
{
    public class GetAllBiblioResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<BibliotecarioDto> Data { get; set; }
    }
}
