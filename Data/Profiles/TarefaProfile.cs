using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Data.Profiles
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<CreateTarefaDTO, Tarefa>()
                .ForMember(t => t.DtCriacao, options => options.MapFrom(dto => DateOnly.Parse(dto.DtCriacao)))
                .ForMember(t => t.DtCompleta, options => options.MapFrom(dto => DateOnly.Parse(dto.DtCompleta)));
            CreateMap<Tarefa, ReadTarefaDTO>();
        }
    }
}
