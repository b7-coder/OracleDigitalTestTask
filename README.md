# Тестовое задание

Инструкция по запуску веб-сервиса

Для запуска требуется версии системы Windows для .net 6 
postgresql выше 14 версии - https://www.postgresql.org/download/windows/


.net 6 runtime - https://dotnet.microsoft.com/en-us/download/dotnet/6.0


ASP.NET Core Runtime 6 - https://dotnet.microsoft.com/en-us/download/dotnet/6.0


Настройте TestTask\WebServiceProject\bin\Debug\net6.0\appsettings.json для подключения к вашей базе данных


"ConnectionStrings": {
    "DatabaseConnection": "Host=localhost;Port=Порт;Database=База;Username=Пользователь;Password=Пароль"
}


Роль указанного пользователя в настройках должна иметь привилегии DDL (Data Definition Language) и DML (Data Manipulation Language)


В каталоге TestTask\WebServiceProject\bin\Debug\net6.0 запустите программу WebServiceProject.exe


В браузере откройте https://localhost:5001/swagger/index.html или http://localhost:5000/swagger/index.html
Страница показывает как нужно отправить запрос. В теле запроса два параметра, 'apiUrl' - куда нужно отправить, 'searchJsonKey' - какое значение сохранить.
При отправке запроса учтите что веб-сервис отправляет get запрос на указанный url


При запуске через Visual Studio адреса localhost:5001 и localhost:5000 изменятся
В таком случае настройте appsettings.json во вкладке обозревателя решений, внутри проекта WebServiceProject
