namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO ответа с JWT-токеном.
    /// </summary>
    public class AuthTokenResponseDto
    {
        /// <summary>
        /// JWT-токен.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
