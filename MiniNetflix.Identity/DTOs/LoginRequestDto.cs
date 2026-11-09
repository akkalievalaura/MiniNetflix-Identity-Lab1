namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO запроса входа (логина).
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль в открытом виде.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
