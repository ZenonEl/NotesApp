# NotesApp

## Описание

NotesApp — простое приложение для создания и управления заметками с возможностью их распределения по категориям.

## Требования к среде разработки

### Установка необходимых компонентов

1. **Установите .NET SDK**:
   - Скачайте и установите .NET SDK с официального сайта: [.NET Download](https://dotnet.microsoft.com/download)

2. **Установите PostgreSQL**:
   - Скачайте и установите PostgreSQL с официального сайта: [PostgreSQL Download](https://www.postgresql.org/download/)

3. **Установите Entity Framework Core CLI**:
   - Откройте терминал и выполните команду:
     ```sh
     dotnet tool install --global dotnet-ef
     ```

4. **Установите TinyMCE**:
   - TinyMCE будет подключен через CDN в представлениях.

### Настройка подключения к базе данных PostgreSQL

1. **Создайте базу данных PostgreSQL**:
   - Откройте терминал и выполните команду:
     ```sh
     sudo -u postgres psql
     ```
   - Введите команду для создания базы данных:
     ```sql
     CREATE DATABASE NotesApp;
     ```

2. **Создайте пользователя и настройте права**:
   - Введите команду для создания пользователя:
     ```sql
     CREATE USER username WITH PASSWORD 'your_password';
     ```
   - Настройте права доступа:
     ```sql
     GRANT ALL PRIVILEGES ON DATABASE NotesApp TO username;
     ```

3. **Настройте файл `appsettings.json`**:
   - Создайте и откройте файл `appsettings.json`, добавьте строку подключения:
     ```json
     {
        "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Database=NotesApp;Username=username;Password=your_password"
        },
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "EncryptionKey": "YourBase16EncodedEncryptionKeyHere"
     }
     ```

### Инструкции по запуску и тестированию основных функций приложения

1. **Клонирование репозитория**:
   - Клонируйте репозиторий с GitHub:
     ```sh
     git clone https://github.com/yourusername/NotesApp.git
     cd NotesApp
     ```

2. **Применение миграций**:
   - Откройте терминал и выполните команду:
     ```sh
     dotnet ef database update
     ```

3. **Запуск приложения**:
   - Запустите приложение с помощью команды:
     ```sh
     dotnet run
     ```

4. **Тестирование основных функций**:
   - Откройте браузер и перейдите по адресу `http://localhost:5187` (или другому порту, если указано иное).
   - **Создание категории**:
     - Введите название категории и нажмите "Создать".
   - **Добавление заметки**:
     - Выберите категорию из выпадающего списка, введите текст заметки и нажмите "Добавить заметку".
   - **Просмотр заметок**:
     - Нажмите на категорию в списке для просмотра всех заметок в этой категории.
   - **Удаление заметки**:
     - На странице просмотра заметок нажмите кнопку "Удалить" рядом с заметкой для её удаления.
   - **Удаление категории**:
     - На странице просмотра заметок нажмите кнопку "Удалить категорию" для удаления категории и всех её заметок.

