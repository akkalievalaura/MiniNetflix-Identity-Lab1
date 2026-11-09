using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Errors;
using MiniNetflix.Identity.Services.Interfaces;

namespace MiniNetflix.Identity.Services.Impl
{
    public class SubscriptionClient : ISubscriptionClient
    {
        private readonly HttpClient _httpClient;
        public SubscriptionClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<SubscriptionDto?> GetSubscriptionAsync(Guid userId)
        {
            try
            {
                using var response = await _httpClient.GetAsync($"api/subscriptions/{userId}");
                if (response.StatusCode == HttpStatusCode.NotFound) return null;
                EnsureSuccess(response);
                return await ReadSubscriptionAsync(response);
            }
            catch (HttpRequestException) { throw External("subscription_service_unavailable", "Не удалось связаться с сервисом подписок"); }
            catch (TaskCanceledException) { throw External("subscription_service_timeout", "Сервис подписок не ответил вовремя"); }
            catch (JsonException) { throw External("subscription_invalid_response", "Сервис подписок вернул некорректный JSON"); }
            catch (NotSupportedException) { throw External("subscription_invalid_response", "Неподдерживаемый формат ответа сервиса подписок"); }
        }

        public async Task<SubscriptionDto> ChangeSubscriptionAsync(Guid userId, ChangeSubscriptionRequestDto request)
        {
            try
            {
                using var response = await _httpClient.PutAsJsonAsync($"api/subscriptions/{userId}", request);
                EnsureSuccess(response);
                return await ReadSubscriptionAsync(response);
            }
            catch (HttpRequestException) { throw External("subscription_service_unavailable", "Не удалось связаться с сервисом подписок"); }
            catch (TaskCanceledException) { throw External("subscription_service_timeout", "Сервис подписок не ответил вовремя"); }
            catch (JsonException) { throw External("subscription_invalid_response", "Сервис подписок вернул некорректный JSON"); }
            catch (NotSupportedException) { throw External("subscription_invalid_response", "Неподдерживаемый формат ответа сервиса подписок"); }
        }

        private static void EnsureSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw External("subscription_service_error", $"Сервис подписок вернул HTTP {(int)response.StatusCode}");
        }

        private static async Task<SubscriptionDto> ReadSubscriptionAsync(HttpResponseMessage response) =>
            await response.Content.ReadFromJsonAsync<SubscriptionDto>()
            ?? throw External("subscription_invalid_response", "Сервис подписок вернул пустой ответ");

        private static ExternalServiceException External(string code, string message) => new(code, message);
    }
}
