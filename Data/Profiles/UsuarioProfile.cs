using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Data.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegisterUsuarioDTO, Usuario>().ForMember(usuario => usuario.UserName, opt => opt.MapFrom(dto => dto.Email));
        }
    }
}
