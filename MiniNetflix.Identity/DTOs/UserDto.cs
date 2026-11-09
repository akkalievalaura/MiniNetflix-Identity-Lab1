namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO пользователя для ответов API.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Публичный идентификатор пользователя (Guid).
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Роль пользователя (строкой).
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Уровень подписки.
        /// </summary>
        public string SubscriptionLevel { get; set; } = string.Empty;
    }
}
