using AutoMapper;

namespace SIGEBI.Application.Profiles
{
    public class PrestamosProfile : Profile
    {
        public PrestamosProfile()
        {
            CreateMap<Domain.Entities.Configuration.Prestamos.Prestamos, Dtos.Configuration.PrestamosDtos.PrestamoDto>().PreserveReferences();
        }
    }
}
