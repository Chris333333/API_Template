# API_Template

A robust .NET 8.0 solution template for building modern, scalable API projects with Entity Framework Core and MySQL/MariaDB support.

This project is designed as a starting point for ASP.NET API development, providing a clean architecture, reusable base controllers, repository patterns, and ready-to-extend infrastructure for real-world applications.

## Key Features

- **Multi-project solution**: Separation of concerns with `API_Template` (API), `App` (application logic), and `Data` (data access)
- **Entity Framework Core**: Pre-configured for both SQL Server and MySQL/MariaDB (Pomelo)
- **Repository & Specification Pattern**: Generic repositories and specifications for flexible, testable data access
- **Base Controllers**: 
  - `BaseApiController` for CRUD and pagination
  - `BaseMappingApiController` for DTO mapping and advanced result shaping using AutoMapper
- **AutoMapper Integration**: Simplifies mapping between entities and DTOs
- **Middleware & Helpers**: Easily extendable for authentication, error handling, logging, and more
- **Mocked ExampleDbContext**: Demonstrates entity relationships and configuration without requiring a real database
- **Ready for Code-First or Database-First**: Scaffold or design your models as needed

## Example: Base Controllers

- **BaseApiController**: Provides generic CRUD, pagination, and utility endpoints for any entity via repository pattern.
- **BaseMappingApiController**: Extends the base controller with AutoMapper-powered endpoints for DTO mapping and paged results.

```csharp
public class BaseMappingApiController(IMapper mapper) : BaseApiController
{
    protected readonly IMapper _mapper = mapper;
    // AddEntity, CreateMappedPagedResult, GetMappedEntityWithSpec, etc.
}
```

## Project Structure

- `API_Template/Controllers/Base/` — Base controllers for rapid API development
- `App/Helpers/` — Utility classes and helpers for specifications, pagination, etc.
- `App/Spec/` — Specification pattern for flexible queries
- `Data/Interfaces/` — Generic repository interfaces
- `Data/Entities/DatabaseDB/` — ExampleDbContext and mock entities

## Getting Started

1. **Clone the repository**
2. **Open in Visual Studio 2022+**
3. **Restore NuGet packages**
4. **Update connection strings** as needed in your DbContext
5. **Add your own entities, DTOs, and business logic**
6. **Extend controllers, repositories, and middleware** to fit your requirements

## Extending the Template

- Add new controllers by inheriting from `BaseApiController` or `BaseMappingApiController`
- Define new entities and DTOs in the `Data` and `App` projects
- Register new repositories and services in the DI container
- Add or customize middleware for authentication, error handling, etc.

## Packages Used

- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Pomelo.EntityFrameworkCore.MySql
- MySql.EntityFrameworkCore
- AutoMapper

## License

See `LICENSE.txt` for details.
