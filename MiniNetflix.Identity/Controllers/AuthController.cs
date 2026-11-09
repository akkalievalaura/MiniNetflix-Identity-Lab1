using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Services.Interfaces;
using System.Security.Claims;

namespace MiniNetflix.Identity.Controllers
{
    /// <summary>
    /// Контроллер для авторизации.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="request">Данные для регистрации.</param>
        /// <returns>Сообщение и публичный идентификатор пользователя.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDto = await _userService.RegisterAsync(request.Email, request.Password);
            return Ok(new RegisterResponseDto
            {
                Message = "Пользователь успешно зарегистрирован",
                UserId = userDto.UserId
            });
        }

        /// <summary>
        /// Вход пользователя.
        /// </summary>
        /// <param name="request">Данные для входа.</param>
        /// <returns>JWT-токен, если вход успешен.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.LoginAsync(request.Email, request.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new AuthTokenResponseDto { Token = token });
        }

        /// <summary>
        /// Возвращает профиль текущего пользователя.
        /// </summary>
        /// <remarks>
        /// Требует заголовок <c>Authorization: Bearer {token}</c>.
        /// </remarks>
        /// <returns>Профиль пользователя.</returns>
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var userDto = await _userService.GetUserProfileAsync(userId);
            return Ok(userDto);
        }

        /// <summary>
        /// Меняет пароль текущего пользователя.
        /// </summary>
        /// <remarks>
        /// Требует заголовок <c>Authorization: Bearer {token}</c>.
        /// </remarks>
        /// <param name="request">Текущий и новый пароль.</param>
        /// <returns>Сообщение об успешной смене пароля.</returns>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult<MessageResponseDto>> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            // TODO (студенты): реализовать логику внутри IUserService.ChangePasswordAsync().
            await _userService.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);

            return Ok(new MessageResponseDto { Message = "Пароль успешно изменён" });
        }
    }


}