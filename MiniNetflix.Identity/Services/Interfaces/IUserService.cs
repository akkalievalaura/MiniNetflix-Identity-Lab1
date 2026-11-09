using MiniNetflix.Identity.DTOs;

namespace MiniNetflix.Identity.Services.Interfaces
{
    /// <summary>
    /// Сервис для регистрации, входа и получения профиля пользователя.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль в открытом виде (будет захэширован перед сохранением).</param>
        /// <returns>Пользователь в виде DTO.</returns>
        Task<UserDto> RegisterAsync(string email, string password);

        /// <summary>
        /// Выполняет вход пользователя и возвращает JWT-токен.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль в открытом виде.</param>
        /// <returns>JWT-токен или <see langword="null"/>, если данные неверны.</returns>
        Task<string?> LoginAsync(string email, string password);

        /// <summary>
        /// Возвращает профиль пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns>Профиль пользователя в виде DTO.</returns>
        Task<UserDto> GetUserProfileAsync(Guid userId);

        /// <summary>
        /// Меняет пароль текущего пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <param name="currentPassword">Текущий пароль.</param>
        /// <param name="newPassword">Новый пароль.</param>
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
