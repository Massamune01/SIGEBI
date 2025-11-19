using SIGEBI.Application.Dtos.Configuration.AdminDtos;

namespace SIGEBI.Web.ViewModels.Admin
{
    public class GetAllAdminsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<AdminDto> Data { get; set; }
    }
}

