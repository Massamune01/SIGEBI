using AutoMapper;

namespace SIGEBI.Application.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Domain.Entities.Configuration.Admin, Dtos.Configuration.AdminDtos.AdminDto>().PreserveReferences();
        }
    }
}
