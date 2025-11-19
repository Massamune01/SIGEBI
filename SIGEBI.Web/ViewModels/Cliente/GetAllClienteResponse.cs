using SIGEBI.Application.Dtos.Configuration.ClienteDtos;

namespace SIGEBI.Web.ViewModels.Cliente
{
    public class GetAllClienteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ClienteDto> Data { get; set; }
    }
}
