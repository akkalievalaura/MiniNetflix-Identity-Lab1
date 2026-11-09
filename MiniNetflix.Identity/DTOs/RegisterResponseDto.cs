namespace MiniNetflix.Identity.DTOs
{
    /// <summary>
    /// DTO ответа на регистрацию.
    /// </summary>
    public class RegisterResponseDto
    {
        /// <summary>
        /// Человекочитаемое сообщение о результате операции.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Публичный идентификатор созданного пользователя.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
