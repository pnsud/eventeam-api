<h1 align="center">EvenTeam API</h1>

<p align="center">
  Учебный Web API на C#: CRUD-сервис для управления мероприятиями.
</p>

<p align="center">
    <a href="https://learn.microsoft.com/aspnet/core/web-api/"><img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=flat-square&logo=dotnet&logoColor=white" alt="ASP.NET Core Web API"></a>
    <a href="https://practicum.yandex.ru/middle-csharp/"><img src="https://img.shields.io/badge/Курс-Яндекс_Практикум-27874B?style=flat-square" alt="Yandex"></a>
</p>

## О проекте

Приложение на [.NET 10](https://dotnet.microsoft.com/download) поднимает HTTP-сервис через [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/). Клиент работает с коллекцией мероприятий: получает список, ищет по id, создаёт, обновляет и удаляет записи.

## Возможности

- `GET /events` — список всех событий
- `GET /events/{id}` — одно событие или **404**, если id нет
- `POST /events` — создать событие, ответ **201 Created** с `Location`
- `PUT /events/{id}` — обновить событие целиком, ответ **204 No Content**
- `DELETE /events/{id}` — удалить событие, ответ **204 No Content**
- Валидация входных данных: обязательные `title`, `startAt`, `endAt`; `endAt` строго позже `startAt`
- Данные живут в памяти процесса: после перезапуска список снова пуст

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download) или выше

## Быстрый старт

```bash
git clone <ссылка на репозиторий>
cd EventApi
dotnet restore
dotnet run --project src/EventApi
```