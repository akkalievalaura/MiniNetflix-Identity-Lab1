namespace MiniNetflix.Identity.Errors
{
    /// <summary>
    /// Базовое исключение приложения (для централизованной обработки).
    /// </summary>
    public abstract class AppException : Exception
    {
        /// <summary>
        /// Код ошибки (короткая строка, удобна для клиентов и логов).
        /// </summary>
        public string Code { get; }

        protected AppException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}

