namespace MiniNetflix.Identity.Models.Args
{
    /// <summary>
    /// Данные запроса для авторизации.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Почта
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public required string Password { get; set; }
    }
}
