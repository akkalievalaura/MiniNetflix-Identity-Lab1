# Лабораторная работа 1: MiniNetflix.Identity

Исходный проект: https://github.com/kirocan/MiniNetflix.Identity

## Запуск

Требуются .NET SDK 10 и PostgreSQL. Для локальной учебной базы с Docker:

```
docker run --name mininetflix-postgres -e POSTGRES_USER=asvc -e POSTGRES_PASSWORD=Passw0rd1 -e POSTGRES_DB=MiniNetflix_Identity -p 5432:5432 -d postgres:16
dotnet restore MiniNetflix.Identity/MiniNetflix.Identity.csproj
dotnet build MiniNetflix.Identity/MiniNetflix.Identity.csproj
dotnet run --project MiniNetflix.Identity/MiniNetflix.Identity.csproj --launch-profile http
```

Swagger: http://localhost:5001/swagger. Существующие миграции и роли добавляются при старте. Если порт занят, изменить порт контейнера и строку подключения в appsettings.json. Учебные пароль БД и JWT-ключ нельзя использовать в production.

## Проверка

1. POST /api/Auth/register, сохранить GUID из ответа.
2. POST /api/Auth/login, передать токен через Authorize в Swagger.
3. POST, GET, PUT, DELETE /api/users/{userId}/profile. Для BirthDate использовать UTC, например `2000-01-01T00:00:00Z`. PUT заменяет все редактируемые поля, пропущенные поля станут null.
4. Проверить повторное создание (409), отсутствующий профиль (404), смену пароля через POST /api/Auth/change-password и старый/новый пароль при входе. Неверный текущий пароль: 400.

## Контракт будущего Subscription

Базовый адрес: http://localhost:5003. GET /api/subscriptions/{userId}: 200 и JSON подписки, 404 означает отсутствие подписки. PUT /api/subscriptions/{userId}: запрос `{"level":"Premium"}`, ответ — JSON подписки: `{"userId":"GUID","level":"Premium","status":"Active","startsAt":"2026-01-01T00:00:00Z","endsAt":null}`. Ошибки сети, некорректный ответ и другие неуспешные статусы отображаются как ExternalServiceException (502). Это учебный контракт, его нужно согласовать с сервисом во второй работе. В исходном Identity нет контроллера, который вызывает этот клиент; его проверяют отдельным тестом с HTTP mock, совместимым с контрактом.

## Ограничение исходного проекта

UserProfileController проверяет авторизацию, но не сравнивает userId в URL с идентификатором в токене. Для production необходимо добавить проверку прав на профиль.
