namespace MiniNetflix.Identity.Errors
{
    /// <summary>
    /// Ошибка бизнес-логики (некорректное действие с точки зрения правил домена).
    /// </summary>
    public class BusinessException : AppException
    {
        public BusinessException(string code, string message) : base(code, message)
        {
        }
    }
}

