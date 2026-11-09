using System.ComponentModel.DataAnnotations;

namespace MiniNetflix.Identity.Data.Models
{
    /// <summary>
    /// Пользователь (сущность базы данных).
    /// </summary>
    public class User
    {
        /// <summary>
        /// Внутренний идентификатор (первичный ключ в БД).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Публичный идентификатор пользователя (Guid). Используется в API и в JWT.
        /// </summary>
        public Guid UserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Почта пользователя (логин).
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Хэш пароля (сам пароль в БД не хранится).
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Уровень подписки пользователя (например: Free).
        /// </summary>
        public string SubscriptionLevel { get; set; } = "Free";

        /// <summary>
        /// Внутренний идентификатор роли (внешний ключ на <see cref="Role"/>).
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public Role Role { get; set; } = null!;

        /// <summary>
        /// Профиль пользователя (дополнительные данные).
        /// </summary>
        public UserProfile? Profile { get; set; }
    }
}