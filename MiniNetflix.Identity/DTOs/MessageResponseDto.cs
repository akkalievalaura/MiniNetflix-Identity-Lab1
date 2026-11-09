namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// Универсальный DTO ответа с сообщением.
    /// </summary>
    public class MessageResponseDto
    {
        /// <summary>
        /// Сообщение для клиента.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}

