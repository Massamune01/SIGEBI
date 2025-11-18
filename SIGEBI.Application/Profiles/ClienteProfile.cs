using AutoMapper;

namespace SIGEBI.Application.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Domain.Entities.Configuration.Cliente, Dtos.Configuration.ClienteDtos.ClienteDto>().PreserveReferences();
        }
    }
}
