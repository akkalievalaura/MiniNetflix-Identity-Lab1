namespace MiniNetflix.Identity.Errors
{
    /// <summary>
    /// Ошибка: конфликт (например, попытка создать уже существующую сущность).
    /// </summary>
    public class ConflictException : BusinessException
    {
        public ConflictException(string code, string message) : base(code, message)
        {
        }
    }
}

