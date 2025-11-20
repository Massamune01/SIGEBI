using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;

namespace SIGEBI.Web.ViewModels.LogOp
{
    public class GetLogOpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LogOperationsDto Data { get; set; }
    }
}
