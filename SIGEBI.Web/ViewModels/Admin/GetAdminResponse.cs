using SIGEBI.Application.Dtos.Configuration.AdminDtos;

namespace SIGEBI.Web.ViewModels.Admin
{
    public class GetAdminsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AdminDto Data { get; set; }
    }
}
