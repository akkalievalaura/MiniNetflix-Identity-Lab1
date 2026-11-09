using MiniNetflix.Identity.Data.Models;

namespace MiniNetflix.Identity.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с пользователями в базе данных.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Ищет пользователя по почте.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <returns>Пользователь или <see langword="null"/>, если не найден.</returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Создаёт нового пользователя.
        /// </summary>
        /// <param name="user">Пользователь (модель базы данных).</param>
        /// <returns>Созданный пользователь.</returns>
        Task<User> CreateUserAsync(User user);

        /// <summary>
        /// Возвращает пользователя по внутреннему идентификатору (PK в БД).
        /// </summary>
        /// <param name="id">Внутренний идентификатор пользователя (int).</param>
        /// <returns>Пользователь.</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Возвращает пользователя по публичному идентификатору (Guid).
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns>Пользователь или <see langword="null"/>, если не найден.</returns>
        Task<User?> GetUserByUserIdAsync(Guid userId);

        /// <summary>
        /// Обновляет хэш пароля пользователя.
        /// </summary>
        /// <param name="id">Внутренний идентификатор пользователя (int).</param>
        /// <param name="passwordHash">Новый хэш пароля.</param>
        Task UpdatePasswordHashAsync(int id, string passwordHash);
    }
}
