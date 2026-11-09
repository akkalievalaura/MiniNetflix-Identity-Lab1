using MiniNetflix.Identity.Data.Models;

namespace MiniNetflix.Identity.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с профилем пользователя в базе данных.
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Возвращает профиль пользователя по внутреннему идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Внутренний идентификатор пользователя (int).</param>
        /// <returns>Профиль или <see langword="null"/>, если не найден.</returns>
        Task<UserProfile?> GetByUserIdAsync(int userId);

        /// <summary>
        /// Создаёт профиль пользователя.
        /// </summary>
        /// <param name="profile">Профиль (модель базы данных).</param>
        /// <returns>Созданный профиль.</returns>
        Task<UserProfile> CreateAsync(UserProfile profile);

        /// <summary>
        /// Обновляет профиль пользователя.
        /// </summary>
        /// <param name="profile">Профиль (модель базы данных).</param>
        /// <returns>Обновлённый профиль.</returns>
        Task<UserProfile> UpdateAsync(UserProfile profile);

        /// <summary>
        /// Удаляет профиль пользователя по внутреннему идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Внутренний идентификатор пользователя (int).</param>
        /// <returns><see langword="true"/>, если профиль удалён, иначе <see langword="false"/>.</returns>
        Task<bool> DeleteByUserIdAsync(int userId);
    }
}

