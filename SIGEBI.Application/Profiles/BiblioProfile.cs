using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SIGEBI.Application.Profiles
{
    public class BiblioProfile : Profile
    {
        public BiblioProfile()
        {
            CreateMap<Domain.Entities.Configuration.Bibliotecarios, Dtos.Configuration.BibliotecariosDtos.BibliotecarioDto>().PreserveReferences();
        }
    }
}
