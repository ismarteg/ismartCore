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

📚 Dependencies
NET 8
AutoMapper
Entity Framework Core


## 🚀 Installation

If used as a submodule or referenced library:

```bash
dotnet add reference ../ismartCore/ismartCore.csproj

Or include it as a NuGet package :

dotnet add package ismartCore

-------------------------------------------
BaseService Example

public class Srv_Clinic : BaseService<tblClinic, DtoClinic>
{
    public Srv_Clinic(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }
}

var dto = new DtoClinic { Name = "Main Clinic" };
var result = _clinicService.SaveItem(dto, userId);