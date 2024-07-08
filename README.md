    # Clean Architecture Test

Project Name: TaxCalc
Project Description: Kerridge systems assessment

## Introduction

#### EF Migrations

Execute this from the top level solution directory that contains the .sln file to add migrations to TaxCalc.

##### Warning

Before executing any dotnet commands ensure you local system environment is set to Local as a fail safe

```bash
setx ASPNETCORE_ENVIRONMENT Local /M
```

##### Add migrations

```bash
dotnet ef migrations add <migration-name> -p .\taxCalc\Infrastructure\infra.data\ -s .\taxCalc\Api\ -o .\taxCalc\Infrastructure\infra.data\Data\
```

##### Remove migrations

```bash
dotnet ef migrations remove -c ApplicationDbContext -p .\taxCalc\Infrastructure\infra.data\ -s taxCalc/Api
```

##### Database Update with environment

```bash
dotnet ef database update -p .\taxCalc\Infrastructure\infra.data\ -s .\taxCalc\Api\ -- --environment=<Environment>
```
