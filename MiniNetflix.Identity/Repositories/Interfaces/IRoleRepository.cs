using MiniNetflix.Identity.Data.Models;

namespace MiniNetflix.Identity.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с ролями в базе данных.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Возвращает все роли.
        /// </summary>
        /// <returns>Список ролей.</returns>
        Task<List<Role>> GetAllAsync();

        /// <summary>
        /// Возвращает роль по внутреннему идентификатору (PK в БД).
        /// </summary>
        /// <param name="id">Внутренний идентификатор роли (int).</param>
        /// <returns>Роль или <see langword="null"/>, если не найдена.</returns>
        Task<Role?> GetByIdAsync(int id);

        /// <summary>
        /// Возвращает роль по имени.
        /// </summary>
        /// <param name="name">Имя роли.</param>
        /// <returns>Роль или <see langword="null"/>, если не найдена.</returns>
        Task<Role?> GetByNameAsync(string name);

        /// <summary>
        /// Создаёт новую роль.
        /// </summary>
        /// <param name="role">Роль (модель базы данных).</param>
        /// <returns>Созданная роль.</returns>
        Task<Role> CreateAsync(Role role);

        /// <summary>
        /// Обновляет существующую роль.
        /// </summary>
        /// <param name="role">Роль (модель базы данных).</param>
        /// <returns>Обновлённая роль.</returns>
        Task<Role> UpdateAsync(Role role);

        /// <summary>
        /// Удаляет роль по внутреннему идентификатору.
        /// </summary>
        /// <param name="id">Внутренний идентификатор роли (int).</param>
        /// <returns><see langword="true"/>, если роль удалена, иначе <see langword="false"/>.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
