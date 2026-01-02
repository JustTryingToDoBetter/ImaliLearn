# ImaliLearn 💰

A personal finance education and budgeting application built with .NET 10 and Blazor WebAssembly, following Clean Architecture principles.

## 🏗️ Architecture

The solution follows **Clean Architecture** with four distinct layers:

```
ImaliLearn/
├── ImaliLearn.API/           # Web API (Controllers, Middleware, Security)
├── ImaliLearn.Application/   # Use Cases (Services, DTOs, Interfaces)
├── ImaliLearn.Domain/        # Core (Entities, Repositories, Constants)
├── ImaliLearn.Infrastructure/# Data Access (EF Core, Identity, Repositories)
├── ImaliLearn.Web/           # Blazor WebAssembly Frontend
└── Tests/                    # Unit Tests
```

## ✨ Features

- **JWT Authentication** with refresh tokens
- **User Registration & Login**
- **Budget Management** (Create, List, Delete)
- **Role-based Authorization** (User, Educator, Admin)
- **Budget Ownership Policies** - Users can only access their own budgets
- **Blazor WebAssembly SPA** with protected routes

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| **API** | ASP.NET Core 10, JWT Bearer Auth |
| **Frontend** | Blazor WebAssembly, Bootstrap 5 |
| **Database** | PostgreSQL, Entity Framework Core |
| **Identity** | ASP.NET Core Identity |
| **Storage** | Blazored.LocalStorage |

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (or use Docker)

### Configuration

1. Update the connection string in `ImaliLearn.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=imalilearn;Username=postgres;Password=yourpassword"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyAtLeast32Characters!",
    "Issuer": "ImaliLearn",
    "Audience": "ImaliLearn",
    "ExpiryMinutes": 60
  }
}
```

### Running the Application

**Terminal 1 - API (Port 5000):**
```bash
cd ImaliLearn.API
dotnet run
```

**Terminal 2 - Blazor Frontend (Port 5001):**
```bash
cd ImaliLearn.Web
dotnet run --urls "http://localhost:5001"
```

### Apply Migrations

```bash
cd ImaliLearn.API
dotnet ef database update --project ../ImaliLearn.Infrastructure
```

## 📡 API Endpoints

### Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and get JWT tokens |
| POST | `/api/auth/refresh` | Refresh access token |
| POST | `/api/auth/logout` | Logout (revoke refresh token) |
| POST | `/api/auth/logout-all` | Logout all sessions |

### Budgets (Requires Authentication)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/budgets` | Get user's budgets |
| POST | `/api/budgets` | Create a new budget |
| DELETE | `/api/budgets/{id}` | Delete a budget |

### Health

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/health` | Health check endpoint |

## 🔐 Security

- **JWT Tokens** stored in LocalStorage
- **Refresh Tokens** for seamless token renewal
- **Password Hashing** with secure algorithms
- **Authorization Policies:**
  - `MustBeAuthenticated` - Requires valid JWT
  - `BudgetOwner` - Ensures user owns the budget

## 📁 Project Structure

### ImaliLearn.API
```
Controllers/
├── AuthController.cs      # Authentication endpoints
├── BudgetController.cs    # Budget CRUD operations
├── HealthController.cs    # Health checks
Middleware/
├── GlobalExceptionMiddleware.cs
Security/
├── Handlers/BudgetOwnerHandler.cs
├── Requirements/BudgetOwnerRequirement.cs
```

### ImaliLearn.Application
```
Auth/
├── JwtTokenService.cs
├── LoginUserService.cs
├── RegisterUserService.cs
├── RefreshTokenService.cs
├── LogoutService.cs
├── PasswordHasher.cs
Budgets/
├── CreateBudgets/CreateBudgetService.cs
├── GetUserBudgets/GetUserBudgetsService.cs
```

### ImaliLearn.Domain
```
Entities/
├── User.cs
├── Budget.cs
├── RefreshToken.cs
Repositories/
├── IBudgetRepository.cs
├── IUserRepository.cs
├── IRefreshTokenRepository.cs
```

### ImaliLearn.Web (Blazor)
```
Pages/
├── Index.razor           # Landing page
├── Login.razor           # Login form
├── Register.razor        # Registration form
├── Budgets.razor         # Budget list (protected)
├── CreateBudget.razor    # Create budget form (protected)
Services/
├── AuthService.cs        # Authentication logic
├── AuthStateProvider.cs  # JWT-based auth state
├── BudgetService.cs      # Budget API calls
├── AuthHttpHandler.cs    # Auto JWT header injection
```

## 🧪 Running Tests

```bash
dotnet test
```

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Built with ❤️ using .NET 10 and Clean Architecture