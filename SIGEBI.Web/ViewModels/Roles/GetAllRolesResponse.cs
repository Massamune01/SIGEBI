using SIGEBI.Application.Dtos.Configuration.RolDtos;

namespace SIGEBI.Web.ViewModels.Roles
{
    public class GetAllRolesResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<RolGetModel> Data { get; set; }
    }
}
