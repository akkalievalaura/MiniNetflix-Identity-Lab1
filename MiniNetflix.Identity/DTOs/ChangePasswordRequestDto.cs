namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO запроса на смену пароля.
    /// </summary>
    public class ChangePasswordRequestDto
    {
        /// <summary>
        /// Текущий пароль.
        /// </summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// Новый пароль.
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}

