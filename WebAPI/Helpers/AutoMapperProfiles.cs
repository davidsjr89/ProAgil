using System.Linq;
using AutoMapper;
using Domain;
using Domain.Identity;
using WebAPI.Dtos;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>()
                    .ForMember(dist => dist.Palestrantes, opt => {
                        opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
                    }).ReverseMap();
            CreateMap<Palestrante, PalestranteDto>()
                    .ForMember(dist => dist.Eventos, opt => {
                        opt.MapFrom(scr => scr.PalestranteEventos.Select(x => x.Evento).ToList());
                    }).ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}