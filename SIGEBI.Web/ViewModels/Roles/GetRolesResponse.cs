using SIGEBI.Application.Dtos.Configuration.RolDtos;

namespace SIGEBI.Web.ViewModels.Roles
{
    public class GetRolesResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public RolGetModel Data { get; set; }
    }
}
