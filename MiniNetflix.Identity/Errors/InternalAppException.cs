namespace MiniNetflix.Identity.Errors
{
    /// <summary>
    /// Внутренняя ошибка приложения (конфигурация, инварианты, неожиданные состояния).
    /// </summary>
    public class InternalAppException : AppException
    {
        public InternalAppException(string code, string message) : base(code, message)
        {
        }
    }
}

