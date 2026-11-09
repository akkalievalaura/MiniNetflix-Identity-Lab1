namespace MiniNetflix.Identity.Errors
{
    /// <summary>
    /// Ошибка: сущность не найдена.
    /// </summary>
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string code, string message) : base(code, message)
        {
        }
    }
}

