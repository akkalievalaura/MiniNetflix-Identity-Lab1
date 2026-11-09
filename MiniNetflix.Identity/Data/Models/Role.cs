namespace MiniNetflix.Identity.Data.Models
{
    /// <summary>
    /// Роль пользователя (сущность базы данных).
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Внутренний идентификатор (первичный ключ в БД).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя роли (например: Client, Admin).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Пользователи, которым назначена эта роль.
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}