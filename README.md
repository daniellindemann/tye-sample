# Project Tye sample application

Gettings started with Tye: https://github.com/dotnet/tye/blob/main/docs/getting_started.md#getting-started

## About this project

This project uses Tye to showcase the inner development loop for a service-oriented or microservice architecture of a web application.
Tye helps to run all application parts and dependencies by lacing them up.

```
+-----------+            +-----------+
|           |            |           |
|    Web    +----------->|    API    |
|           |            |           |
+-----------+            +-----------+
```

The application generates some weather forecast data (api) and shows them on a website.

## Run the application

- Clone the repository, install Tye and run `tye run`
- Open the browser and navigate to `http://127.0.0.1:8000`
