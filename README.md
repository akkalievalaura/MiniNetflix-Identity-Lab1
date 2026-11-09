# MiniNetflix.Identity

Учебный (студенческий) сервис **авторизации/идентификации** для проекта MiniNetflix.

## Что умеет

- Регистрация и вход пользователя (JWT)
- Получение профиля текущего пользователя
- Хранение пользователей и ролей в PostgreSQL
- Авто‑миграции и сидинг ролей при старте (`Client`, `Admin`)
- Заготовки для профиля пользователя (`UserProfile`) и внешнего сервиса подписок (`Subscription`)

## Стек / технологии

- **.NET 10 / ASP.NET Core Web API**
- **Entity Framework Core + Npgsql (PostgreSQL)**
- **JWT Bearer Authentication**
- **Swagger (Swashbuckle)**
- **AutoMapper**

## Быстрый старт

1. Укажи строку подключения к PostgreSQL в `MiniNetflix.Identity/appsettings.json` → `ConnectionStrings:DefaultConnection`.
2. Запусти проект:

```bash
dotnet run --project MiniNetflix.Identity/MiniNetflix.Identity.csproj
```

3. Открой Swagger UI:
   - `http://localhost:<port>/swagger`

## Важно для студентов

В проекте специально оставлены **заглушки** (пустые DTO и `NotImplementedException`) для:

- CRUD профиля пользователя (`UserProfile`)
- HTTP‑клиента к сервису подписок `Subscription` (по умолчанию `http://localhost:5003`)

Задание предполагает, что студенты самостоятельно дополнят DTO и реализуют логику сервисов/репозиториев.

