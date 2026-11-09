namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO ошибки для ответов API.
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// Короткий код ошибки (для клиентской логики/логирования).
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Сообщение об ошибке (для человека).
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}

