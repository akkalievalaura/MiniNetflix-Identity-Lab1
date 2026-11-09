using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Services.Interfaces;

namespace MiniNetflix.Identity.Controllers
{
    /// <summary>
    /// Контроллер для CRUD-операций над профилем пользователя.
    /// </summary>
    [ApiController]
    [Route("api/users/{userId:guid}/profile")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// Возвращает профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns>Профиль пользователя.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> Get(Guid userId)
        {
            // TODO (студенты): реализовать логику в сервисе/репозитории и заполнить поля DTO.
            var result = await _userProfileService.GetAsync(userId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Создаёт профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <param name="request">Данные профиля.</param>
        /// <returns>Созданный профиль.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> Create(Guid userId, [FromBody] CreateUserProfileRequestDto request)
        {
            // TODO (студенты): реализовать логику в сервисе/репозитории и заполнить поля DTO.
            var created = await _userProfileService.CreateAsync(userId, request);
            return CreatedAtAction(nameof(Get), new { userId }, created);
        }

        /// <summary>
        /// Обновляет профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <param name="request">Новые данные профиля.</param>
        /// <returns>Обновлённый профиль.</returns>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> Update(Guid userId, [FromBody] UpdateUserProfileRequestDto request)
        {
            // TODO (студенты): реализовать логику в сервисе/репозитории и заполнить поля DTO.
            var updated = await _userProfileService.UpdateAsync(userId, request);
            return Ok(updated);
        }

        /// <summary>
        /// Удаляет профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid userId)
        {
            // TODO (студенты): реализовать логику в сервисе/репозитории.
            var deleted = await _userProfileService.DeleteAsync(userId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

