using MiniNetflix.Identity.DTOs;

namespace MiniNetflix.Identity.Services.Interfaces
{
    /// <summary>
    /// HTTP-клиент для обращения к сервису подписок (Subscription).
    /// </summary>
    public interface ISubscriptionClient
    {
        /// <summary>
        /// Возвращает подписку пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <returns>Подписка пользователя.</returns>
        Task<SubscriptionDto?> GetSubscriptionAsync(Guid userId);

        /// <summary>
        /// Изменяет подписку пользователя.
        /// </summary>
        /// <param name="userId">Публичный идентификатор пользователя (Guid).</param>
        /// <param name="request">Данные для изменения подписки.</param>
        /// <returns>Обновлённая подписка.</returns>
        Task<SubscriptionDto> ChangeSubscriptionAsync(Guid userId, ChangeSubscriptionRequestDto request);
    }
}

