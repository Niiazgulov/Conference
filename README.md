# Тестовое задание на стажировку

# Брифинг
В рамках тестового задания необходимо реализовать функциональность сбора заявок на конференцию от потенциальных докладчиков.
# Технический стек
При реализации сервиса использованы следующие технологии:
1. Asp.net web api (net8.0);
2. Asp.net dependency injection framework;
3. Dapper.Net + FluentMigrator;
4. PostgreSQL и Npgsql.
# Инструкция по запуску сервиса
1. В файле appsettings.json (папка ConfApps) в разделе "ConnectionStrings" изменить поля "NpgConnection" и "MasterConnection", указав актуальные данные доступа к базе данных PostgreSQL (Server; Port; Database; User Id; Password).
2. Запустить сборку сервиса, нажав соответствующую кнопку в IDE.
3. В открывшемся окне Swagger отправить необходимый запрос.
