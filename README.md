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
> Option 2: Install as NuGet Package 

dotnet add package ismartCore

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


-------------------------------------------
DataBase : 

# Db_BaseContext

`Db_BaseContext` is a custom implementation of `IdentityDbContext` tailored to support ASP.NET Core Identity with extended features and additional application-specific entities.

## 🔧 Purpose

This class serves as the **Entity Framework Core database context** for an application that uses ASP.NET Core Identity. It provides:

- Full support for user/role identity with custom entities (`AppUser`, `AppRole`, `UserRole`)
- Custom entity mappings for many-to-many user-role relationships
- Additional DbSets for application-specific entities like countries, cities, regions, and OTP handling

## 🧱 Inheritance Structure

The class inherits from:

```csharp
IdentityDbContext<AppUser, AppRole, string,
  IdentityUserClaim<string>,
  UserRole,
  IdentityUserLogin<string>,
  IdentityRoleClaim<string>,
  IdentityUserToken<string>>



  This allows for full customization and control over all Identity-related tables.

🔄 OnModelCreating
Overrides OnModelCreating to explicitly configure the many-to-many relationship between users and roles through the UserRole join entity.


builder.Entity<UserRole>(userRole =>
{
    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

    userRole.HasOne(ur => ur.Role)
        .WithMany(r => r.UserRoles)
        .HasForeignKey(ur => ur.RoleId)
        .IsRequired();

    userRole.HasOne(ur => ur.User)
        .WithMany(r => r.UserRoles)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();
});

DbSets
DbSet<tbOTP> – Stores OTP records for login/verification.
DbSet<tbCity> – Cities list, linked to regions/countries.
DbSet<tbRegion> – Regions list, grouped under countries.
DbSet<tbCountry> – Country master data for geographical structuring.

📁 Related Entities
this Classes allready defined :
AppUser : extends IdentityUser
AppRole : extends IdentityRole
UserRole : custom join table for user-role many-to-many relationship
tbOTP, tbCity, tbRegion, tbCountry : your domain models

✅ Benefits
Fully customizable Identity schema
Clean separation of user-role mappings
Easily extensible with your own entities

-------------------------------------------------






BaseService Example

public class Srv_Clinic : BaseService<tblClinic, DtoClinic>
{
    public Srv_Clinic(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }
}

var dto = new DtoClinic { Name = "Main Clinic" };
var result = _clinicService.SaveItem(dto, userId);