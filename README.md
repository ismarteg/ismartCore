# iSmartCore

`iSmartCore` is a reusable .NET Class Library that provides common building blocks for ASP.NET Core projects.  
It includes generic services, repository patterns, soft deletion, DTO mapping, and response wrapping — all ready to plug into your application.

## 🚀 Features

- ✅ BaseEntity with audit fields (CreatedBy, EditedBy, Timestamps)
- ✅ Generic Repository Pattern
- ✅ Unit of Work Pattern
- ✅ Base Service with CRUD operations
- ✅ Soft Delete support
- ✅ AutoMapper or Custom DTO Mapping support
- ✅ Uniform Service Response (`SrvResponse`)

## 📦 Installation

> Option 1: Add reference to the project  
> Option 2: Install as NuGet Package (coming soon)

```bash
dotnet add package iSmart.Core


ismartCore/
│
├── Entities
│ ├── BaseEntity.cs
│ ├── GuidBaseEntity.cs
│ └── ISoftEntity.cs
│
├── Contracts
│ ├── IRepository.cs
│ ├── IUnitOfWork.cs
│ └── IDtoService.cs
│
├── Repositories
│ ├── GenericRepository.cs
│ └── UnitOfWork.cs
│
├── Services
│ ├── BaseService.cs
│ └── EntityExtensions.cs
│
├── Responses
│ ├── SrvResponse.cs
│ └── SaveResult.cs
│
├── Constants
│ └── AppConstants.cs
│
├── Utils
│ ├── IDGenerator.cs
│ └── DateTimeHelper.cs
│
└── ismartCore.csproj
