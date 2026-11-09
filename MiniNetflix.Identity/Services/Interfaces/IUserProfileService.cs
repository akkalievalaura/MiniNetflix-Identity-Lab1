using MiniNetflix.Identity.DTOs;

namespace MiniNetflix.Identity.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с профилем пользователя.
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Возвращает профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns>Профиль пользователя в виде DTO или <see langword="null"/>.</returns>
        Task<UserProfileDto?> GetAsync(Guid userId);

        /// <summary>
        /// Создаёт профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <param name="request">Данные профиля.</param>
        /// <returns>Созданный профиль в виде DTO.</returns>
        Task<UserProfileDto> CreateAsync(Guid userId, CreateUserProfileRequestDto request);

        /// <summary>
        /// Обновляет профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <param name="request">Новые данные профиля.</param>
        /// <returns>Обновлённый профиль в виде DTO.</returns>
        Task<UserProfileDto> UpdateAsync(Guid userId, UpdateUserProfileRequestDto request);

        /// <summary>
        /// Удаляет профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns><see langword="true"/>, если профиль удалён, иначе <see langword="false"/>.</returns>
        Task<bool> DeleteAsync(Guid userId);
    }
}

