# TomNext Portfolio Assets — Backend (TomNextPortfolioAssets.WebApi)

✅ **Overview**

This is the backend API for the TomNext Portfolio Assets project. It is an ASP.NET Core Web API targeting **.NET 8.0** that uses **Entity Framework Core** with **PostgreSQL (Npgsql)** as the database provider. Migrations (and seed data) are kept in the EF project (`TomNextPortfolioAssets.EF`).

---

## Requirements

- .NET 8 SDK (install from https://dotnet.microsoft.com)
- PostgreSQL server (or managed PostgreSQL)

## Key Configuration Overview

Configuration for this project is read from environment variables. A custom provider maps environment variables with underscores to configuration keys where underscores are replaced by `:`.

- `ConnectionStrings` — Base connection string (without credentials if you prefer to inject them separately).
- `InjectDBCredentialFromEnvironment` — When `true`, credentials will be appended from `AspNetCoreDbUserName` and `AspNetCoreDbPassword`.
- `AspNetCoreDbUserName` and `AspNetCoreDbPassword` — Database username/password when credentials are injected.
- `SkipDbConnectIfNoConnectionString` — When `true` and `ConnectionStrings` is empty, DB connection and migrations are skipped.

Note: Local development also loads environment variables from `Properties/launchSettings.json` (used by `DesignTimeDbContextFactory`).
