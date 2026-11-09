using AutoMapper;
using MiniNetflix.Identity.Data.Models;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Errors;
using MiniNetflix.Identity.Repositories.Interfaces;
using MiniNetflix.Identity.Services.Interfaces;

namespace MiniNetflix.Identity.Services.Impl
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _users;
        private readonly IUserProfileRepository _profiles;
        private readonly IMapper _mapper;

        public UserProfileService(IUserRepository users, IUserProfileRepository profiles, IMapper mapper)
        {
            _users = users;
            _profiles = profiles;
            _mapper = mapper;
        }

        private async Task<User> FindUserAsync(Guid userId) =>
            await _users.GetUserByUserIdAsync(userId)
            ?? throw new NotFoundException("user_not_found", "Пользователь не найден");

        public async Task<UserProfileDto?> GetAsync(Guid userId)
        {
            var user = await FindUserAsync(userId);
            var profile = await _profiles.GetByUserIdAsync(user.Id)
                ?? throw new NotFoundException("profile_not_found", "Профиль пользователя не найден");
            return _mapper.Map<UserProfileDto>(profile);
        }

        public async Task<UserProfileDto> CreateAsync(Guid userId, CreateUserProfileRequestDto request)
        {
            var user = await FindUserAsync(userId);
            if (await _profiles.GetByUserIdAsync(user.Id) != null)
                throw new ConflictException("profile_already_exists", "Профиль пользователя уже существует");
            var profile = _mapper.Map<UserProfile>(request);
            profile.UserId = user.Id;
            var created = await _profiles.CreateAsync(profile);
            return _mapper.Map<UserProfileDto>(created);
        }

        public async Task<UserProfileDto> UpdateAsync(Guid userId, UpdateUserProfileRequestDto request)
        {
            var user = await FindUserAsync(userId);
            var profile = await _profiles.GetByUserIdAsync(user.Id)
                ?? throw new NotFoundException("profile_not_found", "Профиль пользователя не найден");
            _mapper.Map(request, profile);
            var updated = await _profiles.UpdateAsync(profile);
            return _mapper.Map<UserProfileDto>(updated);
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var user = await FindUserAsync(userId);
            if (!await _profiles.DeleteByUserIdAsync(user.Id))
                throw new NotFoundException("profile_not_found", "Профиль пользователя не найден");
            return true;
        }
    }
}
