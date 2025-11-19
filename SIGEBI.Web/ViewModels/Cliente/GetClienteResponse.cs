using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;

namespace SIGEBI.Web.ViewModels.Cliente
{
    public class GetClienteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ClienteDto Data { get; set; }
    }
}
