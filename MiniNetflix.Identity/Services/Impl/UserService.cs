using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MiniNetflix.Identity.Constants;
using MiniNetflix.Identity.Data.Models;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Repositories.Interfaces;
using MiniNetflix.Identity.Services.Interfaces;
using MiniNetflix.Identity.Errors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniNetflix.Identity.Services.Impl
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<UserDto> RegisterAsync(string email, string password)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
                throw new ConflictException("user_already_exists", "Пользователь с такой почтой уже существует");

            var defaultRole = await _roleRepository.GetByNameAsync(AppRoles.Client);
            if (defaultRole == null)
                throw new InternalAppException("default_role_missing", $"Не найдена роль по умолчанию '{AppRoles.Client}'. Перезапусти сервис, чтобы выполнились миграции/сидинг.");

            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                SubscriptionLevel = "Free",
                RoleId = defaultRole.Id
            };

            var result = await _userRepository.CreateUserAsync(user);
            result = await _userRepository.GetUserByIdAsync(result.Id);
            return _mapper.Map<UserDto>(result);
        }

        /// <inheritdoc />
        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return GenerateJwtToken(user);
        }

        /// <inheritdoc />
        public async Task<UserDto> GetUserProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByUserIdAsync(userId);
            if (user == null)
                throw new NotFoundException("user_not_found", "Пользователь не найден");
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc />
        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetUserByUserIdAsync(userId)
                ?? throw new NotFoundException("user_not_found", "Пользователь не найден");
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new BusinessException("incorrect_current_password", "Текущий пароль указан неверно");
            var hash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdatePasswordHashAsync(user.Id, hash);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("SubscriptionLevel", user.SubscriptionLevel)
            };

            var token = new JwtSecurityToken(
                issuer: "MiniNetflix.Identity",
                audience: "MiniNetflix",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
