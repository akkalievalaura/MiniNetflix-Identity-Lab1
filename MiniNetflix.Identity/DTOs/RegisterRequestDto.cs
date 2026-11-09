namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO запроса регистрации.
    /// </summary>
    public class RegisterRequestDto
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
