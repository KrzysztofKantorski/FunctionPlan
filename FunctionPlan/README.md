## Project Overview

This platform enables users to create, discover, and join meetings and events. It leverages modern backend design patterns to ensure high performance, security, and maintainability.



### Key Features
* **Authentication & Security:** JWT Access & Refresh Token flow, **OAuth 2.0 (Google Login)**, and email verification via OTP (One-Time Password).
* **Caching & In-Memory Storage:** **Redis** for temporary OTP verification codes with automatic Time-To-Live (TTL) expiration.
* **CQRS Pattern:** Hybrid data access architecture using **EF Core** for commands and **Dapper** for read queries.
* **Geolocation Integration:** Interactive map and coordinate selection using **Mapbox API**.
* **Cloud Media Storage:** Image upload and management integrated with **Azure Blob Storage** (emulated locally via **Azurite**).



## Technologies & Tools

### Backend
* **Framework:** .NET 8 / ASP.NET Core REST API
* **Architecture:** Clean Architecture, CQRS (Command Query Responsibility Segregation)
* **Databases & ORM:** PostgreSQL, Entity Framework Core (Commands), Dapper (Queries), Redis (OTP Cache)
* **Authentication:** JWT (JSON Web Tokens), Google OAuth 2.0, BCrypt
* **Testing:** xUnit, Moq
* **External Services:** Mapbox API, Ethereal Email (SMTP Testing), Azurite (Azure Storage Emulator)



## Architecture

The backend follows the **Clean Architecture** principles, separation of concerns, and dependency injection across four core layers:

* **Domain:** Contains core enterprise business entities, value objects, and domain exceptions without external dependencies.
* **Application:** Implements use cases via CQRS. Commands use EF Core for data modification, while Queries use Dapper for optimized SQL reads.
* **Infrastructure:** External integrations including PostgreSQL database contexts, Redis cache services, Azure Blob Storage client (Azurite), and SMTP email handlers.
* **Presentation (API):** ASP.NET Core Web API controllers handling HTTP requests, route validation, middleware error interceptors, and JWT middleware.



## Prerequisites

Ensure you have the following installed on your local environment:

* .NET 8.0 SDK
* Docker Desktop (required for PostgreSQL, Redis, and Azurite)


## Getting Started
1. Clone the Repository
   
```
git clone (https://github.com/KrzysztofKantorski/FunctionPlan/tree/main/FunctionPlan))
```

```
cd FunctionPlan
```


2. Configure Application Settings

Create .env file in project root directory

```
CONN_STRING= your POSTGRESQL connection string

JWT_SECRET= your JWT secret
JWT_EXPIRY_MINUTES= your expiry time

REFRESH_TOKEN_EXPIRY_DAYS= your expiry days

SMTP_HOST= SMTP server address
SMTP_PORT= server port
SMTP_USERNAME= login server name
SMTP_PASSWORD= server password
SENDER_USERNAME= name displayed right after email address
SENDER_EMAIL= sender email address

REDIS_CONN_STRING= your redis connection string

BLOB_CONNECTION_STRING=  your connection string
BLOB_AVATARS_CONTAINER= avatar container name
BLOB_MEETINGS_CONTAINER=  meeting container name

GOOGLE_CLIENT_ID= public identificator to communicate with google OAuth 2.0 services
```


4. Run Database Migrations
Apply EF Core migrations to initialize the PostgreSQL database schema:

```
cd src/API
```

```
dotnet ef database update
```

5. Launch the Application
   
```
dotnet run
```

