using AutoMapper;

namespace SIGEBI.Application.Profiles
{
    public class LogOpProfile : Profile
    {
        public LogOpProfile()
        {
            CreateMap<Domain.Base.LogOperations, Dtos.Configuration.LogOperationsDtos.LogOperationsDto>().PreserveReferences();
        }
    }
}
