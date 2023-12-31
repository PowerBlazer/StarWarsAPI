# Каталог персонажей Star Wars API (версия 1)

Это репозиторий для проекта "Каталог персонажей Star Wars API". Здесь вы найдете API для управления персонажами Star Wars с поддержкой авторизации и версионностью.

## Технологии

- ASP.NET Core
- Entity Framework Core
- SQLite
- JWT Authentication

## Установка

1. Склонируйте репозиторий на вашу локальную машину.
2. Откройте проект в вашей IDE.
3. Убедитесь, что у вас установлен .NET SDK версии 7.0 или выше.
4. В корневой директории проекта выполните команду `dotnet restore` для восстановления необходимых пакетов NuGet.
5. Запустите проект с помощью команды `dotnet run`.

## Использование

После успешного запуска проекта, вы сможете взаимодействовать с API по адресу: `http://localhost:5216`.

### Регистрация и аутентификация

- Для регистрации нового пользователя отправьте POST-запрос на `http://localhost:5216/api/v1/Authorization/register` с JSON-телом, содержащим email и password пользователя.
- Для аутентификации пользователя отправьте POST-запрос на `http://localhost:5216/api/v1/Authorization/login` с JSON-телом, содержащим email и password пользователя. В ответ вы получите токен авторизации.

### Получение списка персонажей

- Для получения списка всех персонажей отправьте GET-запрос на `http://localhost:5216/api/v1/character/characters`.
- Для получения информации о конкретном персонаже отправьте GET-запрос на `http://localhost:5216/api/v1/character/{id}`, где `{id}` - идентификатор персонажа.

### Создание, обновление и удаление персонажей

- Для создания нового персонажа отправьте POST-запрос на `http://localhost:5216/api/v1/character` с JSON-телом, содержащим данные о персонаже.
- Для обновления информации о персонаже отправьте PUT-запрос на `http://localhost:5216/api/v1/character/{id}`, где `{id}` - идентификатор персонажа, с JSON-телом, содержащим обновленные данные о персонаже.
- Для удаления персонажа отправьте DELETE-запрос на `http://localhost:5216/api/v1/character/{id}`, где `{id}` - идентификатор персонажа.

### Документация Swagger

API также предоставляет документацию Swagger, которая позволяет исследовать и протестировать API. Чтобы получить доступ к Swagger UI, перейдите по адресу: `http://localhost:5216/swagger`.
