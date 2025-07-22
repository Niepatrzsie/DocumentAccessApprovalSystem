# Document Access Approval System

This is a simple REST API built with **ASP.NET Core** using **Clean Architecture** and **CQRS**.  
The goal was to model a basic approval workflow for internal documents, focusing on domain logic and code structure.

---

##  Project Structure & Key Design Decisions

The project is split into several layers to separate concerns:

- **API/** – ASP.NET Core controllers and Swagger config
- **Application/** – Contains commands, queries, DTOs, interfaces, MediatR handlers
- **Domain/** – Core business logic: entities, enums, validation
- **Infrastructure/** – EF Core context, repositories, in-memory database, seeding
- **Tests/** – xUnit tests for business logic (handlers), using Moq and FluentAssertions

### Key design choices:
- Used **CQRS** and **MediatR** to split read/write logic and keep things simple to test
- Business rules are encapsulated inside entities (e.g. `Approve()`, `Reject()`)
- Repositories abstract EF Core and make logic testable
- DTOs are used to decouple domain models from the API

---

##  Required NuGet Packages

> Installed via NuGet CLI or Visual Studio NuGet Manager

### Main project (`DocumentAccessApprovalSystem`)
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.InMemory`
- `MediatR`
- `MediatR.Extensions.Microsoft.DependencyInjection`
- `Swashbuckle.AspNetCore` *(for Swagger)*

### Test project (`Tests`)
- `xunit`
- `xunit.runner.visualstudio`
- `FluentAssertions`
- `Moq`

### Assumptions:
- The application assumes a fixed set of users (Users and Approvers) without authentication – requests contain UserId
- Document access requests are always initiated by UserRole.User, and reviewed by UserRole.Approver
- Used InMemory EF Core provider to simplify testing and seeding
- Domain validation is implemented inline (e.g. empty reason throws), but no FluentValidation yet

### Sample seeded data
When the app starts, it seeds:
5 Users
1 Approver
3 Documents
5 Access Requests
You can inspect these using Swagger or a temporary debug controller.

### What I would improve if I had more time
- Add input validators (e.g. using FluentValidation) for commands and queries to ensure consistent request shape and rules
- Add stricter role-based permission checks, so only Approvers can take decisions and Users can only view their own requests
- Improve test coverage for edge cases like invalid user/document IDs, double-approval, etc.
- Refactor some DTOs for better clarity and maybe structure responses differently (e.g. nested UserDto, DocumentDto inside detail views)
