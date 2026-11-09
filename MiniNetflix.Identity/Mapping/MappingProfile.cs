using AutoMapper;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Data.Models;

namespace MiniNetflix.Identity.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.SubscriptionLevel, opt => opt.MapFrom(src => src.SubscriptionLevel))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
