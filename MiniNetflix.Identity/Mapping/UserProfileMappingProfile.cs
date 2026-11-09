using AutoMapper;
using MiniNetflix.Identity.Data.Models;
using MiniNetflix.Identity.DTOs;

namespace MiniNetflix.Identity.Mapping
{
    /// <summary>
    /// Профиль AutoMapper для профиля пользователя.
    /// </summary>
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<CreateUserProfileRequestDto, UserProfile>();
            CreateMap<UpdateUserProfileRequestDto, UserProfile>();
        }
    }
}

