namespace MiniNetflix.Identity.Models.Args
{
    /// <summary>
    /// Данные запроса регистрации.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Почта.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public required string Password { get; set; }
    }
}
