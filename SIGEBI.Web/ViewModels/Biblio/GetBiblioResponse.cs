using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;

namespace SIGEBI.Web.ViewModels.Biblio
{
    public class GetBiblioResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BibliotecarioDto Data { get; set; }
    }
}
