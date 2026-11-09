namespace MiniNetflix.Identity.Errors
{
    /// <summary>
    /// Ошибка при обращении к внешнему сервису (например, Subscription).
    /// </summary>
    public class ExternalServiceException : AppException
    {
        public ExternalServiceException(string code, string message) : base(code, message)
        {
        }
    }
}

