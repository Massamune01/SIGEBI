using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;

namespace SIGEBI.Web.ViewModels.LogOp
{
    public class GetAllLogOpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<LogOperationsDto> Data { get; set; }
    }
}
