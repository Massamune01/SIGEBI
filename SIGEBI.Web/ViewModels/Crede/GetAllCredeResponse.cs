using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;

namespace SIGEBI.Web.ViewModels.Crede
{
    public class GetAllCredeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<CredencialesGetModel> Data { get; set; }
    }
}
