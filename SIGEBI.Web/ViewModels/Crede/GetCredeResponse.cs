using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;

namespace SIGEBI.Web.ViewModels.Crede
{
    public class GetCredeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public CredencialesGetModel Data { get; set; }
    }
}
