using AutoMapper;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Profiles
{
    public class LibroProfile : Profile
    {
        public LibroProfile()
        {
            CreateMap<Libro, LibroDto>().PreserveReferences();
        }
    }
}
