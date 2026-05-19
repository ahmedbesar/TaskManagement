# 📋 Task Management API

A **Project & Task Management REST API** built with **.NET 9** using **Clean Architecture**, **CQRS (MediatR)**, **JWT Authentication**, and **Entity Framework Core**.

---

## 🏛️ Architecture Overview

The solution follows **Clean Architecture** with a strict dependency rule — inner layers have zero knowledge of outer layers.

```
TaskManagement/
├── TaskManagement.Core/             # Domain Layer (no external dependencies)
│   ├── Entities/                    # Domain entities with static factory methods
│   ├── Enums/                       # Domain enums (TaskItemStatusEnum, TaskItemPriorityEnum)
│   ├── Repositories/                # Repository interfaces
│   ├── Interfaces/                  # Service contracts (IJwtTokenService)
│   └── Consts/                      # Domain constants (max lengths, etc.)
│
├── TaskManagement.Application/      # Application Layer
│   ├── Commands/                    # CQRS Write operations (Auth, Projects, Tasks)
│   ├── Queries/                     # CQRS Read operations (Projects, Tasks)
│   ├── Handlers/                    # MediatR Command & Query handlers
│   │   ├── Commands/
│   │   └── Queries/
│   ├── Validators/                  # FluentValidation validators
│   ├── Behaviors/                   # MediatR pipeline behaviors
│   │   ├── ValidationBehavior       # Auto-validates every request
│   │   ├── LoggingBehavior          # Logs request/response
│   │   └── UnhandledExceptionBehavior
│   ├── Mappers/                     # Manual static mappers (no AutoMapper)
│   ├── Responses/                   # readonly record struct DTOs
│   ├── Wrappers/                    # Generic ApiResponse<T> wrapper
│   └── Extensions/                  # ApplicationServiceRegistration
│
├── TaskManagement.Infrastructure/   # Infrastructure Layer
│   ├── Data/
│   │   ├── AppDbContext.cs          # EF Core DbContext
│   │   ├── Configurations/          # Fluent API entity configurations
│   │   └── Migrations/              # EF Core migrations
│   ├── Repositories/                # Concrete repository implementations
│   │   ├── RepositoryBase<T>        # Generic base repository
│   │   ├── ProjectRepository
│   │   ├── TaskRepository
│   │   └── UserRepository
│   ├── Services/
│   │   └── JwtTokenService          # JWT generation using HMAC-SHA256
│   └── Extensions/                  # InfrastructureServiceRegistration
│
└── TaskManagement.Api/              # Presentation Layer
    ├── Controllers/                 # AuthController, ProjectsController, TasksController
    ├── Middleware/                  # GlobalExceptionMiddleware
    ├── Extensions/                  # SwaggerExtensions (JWT support)
    ├── Properties/
    │   └── launchSettings.json      # Opens Swagger on run
    ├── appsettings.json
    └── Program.cs
```

---

## 🔑 Key Design Decisions

| Concern | Approach |
|---|---|
| Architecture | Clean Architecture (4 layers) |
| CQRS | MediatR — Commands & Queries separated |
| Validation | FluentValidation via MediatR pipeline behavior |
| Mapping | Manual static mappers (`ProjectMapper`, `TaskMapper`) |
| ORM | Entity Framework Core 9 with SQL Server |
| Authentication | JWT Bearer Tokens (HMAC-SHA256, 2hr expiry) |
| Password Hashing | BCrypt.Net-Next |
| Error handling | Result pattern (`FluentResults`) + `ApiResponse<T>` |
| DTOs | `readonly record struct` for memory efficiency |
| Entity creation | Static factory methods (`Entity.Create(...)`) |
| Response format | Consistent `ApiResponse<T>` for all endpoints |

---

## ⚙️ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or **SQL Server LocalDB** (included with Visual Studio)
- [Entity Framework Core CLI Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## 🚀 Setup & Running

### 1. Clone / Open the project

```bash
cd TaskManagement
```

### 2. Configure Connection String

Open `TaskManagement.Api/appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagementDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "SuperSecretKeyThatIsAtLeast32BytesLongForTaskManagementApi!",
    "Issuer": "TaskManagementApi",
    "Audience": "TaskManagementUsers"
  }
}
```

> **Tip:** The default connection string uses **LocalDB** — no installation needed if you have Visual Studio installed.

### 3. Apply Database Migrations

```bash
dotnet ef database update -p TaskManagement.Infrastructure -s TaskManagement.Api
```

### 4. Run the API

```bash
cd TaskManagement.Api
dotnet run
```

> **Note:** The browser will **automatically open** the Swagger UI at `http://localhost:5029/swagger`.

---

## 🔐 Authentication Flow

The API uses **stateless JWT Bearer authentication**. The token is **not stored on the server** — it is the client's responsibility to store and attach the token on every request.

```
Client                         API
  │                              │
  │── POST /api/auth/register ──>│  Hash password with BCrypt
  │                              │  Create user in DB
  │<──── { token: "..." } ──────│  Return signed JWT
  │                              │
  │── POST /api/auth/login ─────>│  Verify BCrypt password
  │<──── { token: "..." } ──────│  Return signed JWT
  │                              │
  │── GET /api/projects ────────>│  [Authorize] — Validate JWT
  │   Authorization: Bearer ...  │  Extract UserId from claims
  │<──── [ projects ] ──────────│  Return only user's data
```

**Token claims** embedded inside the JWT:
- `sub` — User ID
- `email` — User email
- `name` — Username
- `jti` — Unique token ID

---

## 📡 API Endpoints

### Auth
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `POST` | `/api/auth/register` | ❌ | Register a new user |
| `POST` | `/api/auth/login` | ❌ | Login and get JWT token |

### Projects
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/projects` | ✅ | Get all user's projects |
| `GET` | `/api/projects/{id}` | ✅ | Get project (with tasks) by ID |
| `POST` | `/api/projects` | ✅ | Create a new project |
| `PUT` | `/api/projects/{id}` | ✅ | Update a project |
| `DELETE` | `/api/projects/{id}` | ✅ | Delete a project |

### Tasks
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| `GET` | `/api/projects/{projectId}/tasks` | ✅ | Get all tasks in a project |
| `POST` | `/api/projects/{projectId}/tasks` | ✅ | Create a task in a project |
| `PUT` | `/api/tasks/{id}/status` | ✅ | Update a task's status |
| `DELETE` | `/api/tasks/{id}` | ✅ | Delete a task |

---

## 📦 NuGet Packages

| Package | Layer | Purpose |
|---------|-------|---------|
| `MediatR` | Application | CQRS + Pipeline behaviors |
| `FluentValidation` | Application | Request validation |
| `FluentResults` | Application + Core | Result/Error pattern |
| `BCrypt.Net-Next` | Application | Password hashing |
| `Microsoft.EntityFrameworkCore.SqlServer` | Infrastructure | SQL Server ORM |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | Infrastructure | JWT middleware |
| `Swashbuckle.AspNetCore` | API | Swagger / OpenAPI UI |

---

## 🔄 Response Format

All endpoints return a consistent `ApiResponse<T>` envelope:

**Success:**
```json
{
  "succeeded": true,
  "message": "Login successful.",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIs...",
    "email": "user@example.com",
    "userName": "johndoe"
  },
  "errors": []
}
```

**Failure:**
```json
{
  "succeeded": false,
  "message": null,
  "data": null,
  "errors": ["Invalid email or password."]
}
```
