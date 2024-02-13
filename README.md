    # Clean Architecture Test

Project Name: Athena
Project Description: The best second hand store in SA

## Introduction

##### Docker compose

```
docker-compose -f docker-compose-development.yml up
```

#### EF Migrations

Execute this from the top level solution directory that contains the .sln file to add migrations to CrmContext.

##### Warning

Before executing any dotnet commands ensure you local system environment is set to Local as a fail safe

```bash
setx ASPNETCORE_ENVIRONMENT Local /M
```

##### Add migrations

```bash
dotnet ef migrations add <migration-name> -p .\limitlesscare_api\Infrastructure\infra.data\ -s .\limitlesscare_api\Api\ -o .\limitlesscare_api\Infrastructure\infra.data\Data\
```

##### Remove migrations

```bash
dotnet ef migrations remove -c ApplicationDbContext -p .\limitlesscare_api\Infrastructure\infra.data\ -s limitlesscare_api/Api
```

##### Database Update with environment

```bash
dotnet ef database update -p .\limitlesscare_api\Infrastructure\infra.data\ -s .\limitlesscare_api\Api\ -- --environment=<Environment>
```
