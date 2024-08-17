# Ozon Route 256 Junior C# developer project

## Goal

**Clean Architecture REST API service for delivery goods**

## Configuration

* Create .env file next to docker-compose.yaml or in other directory and fill it :

```shell
touch .env
echo "# PostgreSQL
PG_PORT={YOUR_PG_PORT | 5432}

# Redis
REDIS_PASSWORD={YOUR_REDIS_PASSWORD}
REDIS_USER={YOUR_REDIS_USER}
REDIS_PORT={YOUR_REDIS_PORT | 6379}

# ElasticSearch and Kibana
ELASTIC_PASSWORD={YOUR_ES_PASSWORD}
KIBANA_PASSWORD={YOUR_KIBANA_PASSWORD}
STACK_VERSION=8.15.0
ES_PORT={YOUR_ES_PORT | 9200}
KIBANA_PORT={YOUR_KIBANA_PORT | 5601}" > .env
```

* In case your ES_PORT is different from default 9200 change src/OzonRoute.Api/appsettings.json file

```json
"ElasticSearchOptions" : {
    "NodeHost": "http://127.0.0.1:{ES_PORT}"
  }
```

* In order to use another .env file location change docker-compose.yaml configuration :

```yaml
env_file:
      - {PATH_TO_ENV_FILE}
```

* Or use --env-file flag with docker-compose when running :

```shell
docker compose --env-file {PATH_TO_ENV_FILE} up -d
```

* Create .txt file with POSTGRES_PASSWORD in it :

```shell
touch {your_path}/pg_password.txt
echo "your_password" > {your_path}/pg_password.txt
```

* Change secrets configuration in docker-compose.yaml :

```yaml
secrets:
  db_password:
    file: {your_path}/pg_password.txt
```

* Create secrets.json and claim UserSecretsId :

```shell
cd src/OzonRoute.Api
dotnet user-secrets init
grep "UserSecretsId" BirthdayNotificationsBot.csproj
```

```shell
<UserSecretsId>{YOUR_USER_SECRETS_ID}</UserSecretsId>
```

* Fill data :

```shell
echo '{
    "DalOptions": {
        "PostgreSQLOptions": {
            "ConnectionString": "USER ID=postgres; Password={PG_PASSWORD}; Host=localhost;Port={PG_PORT};Database=ozon-route;Pooling=true"
        },
        "RedisCacheOptions": {
            "ConnectionString": "127.0.0.1:{REDIS_PORT}",
            "User": {REDIS_USER},
            "Password": {REDIS_PASSWORD}"
        }
    }
}
' > ~/.microsoft/usersecrets/{YOUR_USER_SECRETS_ID}/secrets.json
```

## Running

* **Setup infrastructure with docker-compose**

```shell
docker-compose up -d
```

* **Run .net project**

```shell
cd src/OzonRoute.Api
dotnet run
```

## Technology stack

| Technology  | .NET Tools | Tools
| --------- | --------- | --------- |
| ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)  | ![ASP.NET](https://img.shields.io/badge/ASP.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)  ![XUNIT](https://img.shields.io/badge/XUNIT-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)  |   |
| ![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)  | ![Npgsql](https://img.shields.io/badge/Npgsql-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)  ![Dapper](https://img.shields.io/badge/Dapper-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![FluentMigrator](https://img.shields.io/badge/FluentMigrator-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)|   |
| ![Redis](https://img.shields.io/badge/redis-%23DD0031.svg?style=for-the-badge&logo=redis&logoColor=white)  | ![StackExchange.Redis](https://img.shields.io/badge/StackExchange.Redis-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) |   |
| ![ElasticSearch](https://img.shields.io/badge/-ElasticSearch-005571?style=for-the-badge&logo=elasticsearch) | ![Serilog](https://img.shields.io/badge/Serilog-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)  | ![Kibana](https://a11ybadges.com/badge?logo=kibana)  |
| ![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)  |  |   |