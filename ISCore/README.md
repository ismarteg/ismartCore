📚 Dependencies
* NET 8
* AutoMapper
* Entity Framework Core

# Overview

This package provides a **Class Library (DLL)** that contains a ready-to-use **Entities Layer** and **DbContext** implementation, built on top of **ASP.NET Core Identity** and **Entity Framework Core**.  

It is designed to help developers **quickly set up user management, roles, OTP verification, and location entities (countries, cities, regions)** without reinventing the wheel.  

The library also provides **base entities and contracts** that can be reused or extended across different projects, making development faster and more consistent.

---




## 🔧 What is included?
- **Db_BaseContext**  
  A customizable database context that integrates with ASP.NET Core Identity and includes additional entities such as OTP, City, Country, and Region.  

- **Entities**  
  - **Users** → `AppUser`, `AppRole`, `UserRole`, `tbOTP`  
  - **Places** → `tbCountry`, `tbCity`, `tbRegion`  
  - **Base Classes** → `BaseEntity`, `GuidBaseEntity`  
  - **Contracts** → `IAppUser`, `ISoftEntity`  

- **Role Constants** → Predefined roles through `Cnst_Roles`  

---

## 🎯 Key Features
- 🔑 **Identity-ready**: Built-in integration with ASP.NET Core Identity.  
- 📦 **Reusable Entities**: Predefined models for users, roles, OTP, and locations.  
- 🧩 **Extensible**: Easily extend `AppUser`, `AppRole`, or `BaseEntity` for custom business needs.  
- 🗃 **Base Entities**: Includes both `int` and `Guid` primary key strategies.  
- 🛡 **Soft Delete Support**: via `ISoftEntity`.  
- ⚡ **Faster Development**: Eliminates repetitive setup for common entities and configurations.  

---

## 🚀 Use Cases
- Applications requiring **user authentication and role management**.  
- Systems needing **OTP verification** (e.g., SMS/Email login confirmation).  
- Apps that rely on **location data** (countries, cities, regions).  
- Projects that want to **standardize entities** with base classes (`BaseEntity`, `GuidBaseEntity`).  
- Multi-tenant or distributed systems needing **GUID-based keys**.  

---

## ✅ Benefits
- **Saves development time** by providing ready-made entities and DbContext setup.  
- **Improves consistency** across projects with shared contracts and base entities.  
- **Extremely flexible**: you can use the entities as-is or inherit and extend them.  
- **Compatible** with any ASP.NET Core project using EF Core.  

---

## Project Structure: iSCore

The `iSCore` project is organized into several main layers.  
Each layer has a specific responsibility and is designed to keep the solution modular, maintainable, and extensible.

---

## 📂 Project Tree

```plaintext
iSCore/
│
├── DataBase
│   ├── Entities              # Core entity classes (User, Role, Country, City, Region, etc.)
│   ├── Db_BaseContext.cs     # Main EF Core DbContext with Identity integration
│   └── Seed.cs               # Seeds initial data (roles, users, reference data)
│
├── DAL
│   ├── Interfaces
│   │   ├── IRepository.cs    # Generic repository contract
│   │   ├── IUnitOfWork.cs    # Unit of Work contract
│   ├── GenericRepository.cs  # Generic repository implementation
│   ├── UnitOfWork.cs         # Unit of Work implementation
│   └── UnitOfWorkResult.cs   # Wrapper for repository operations result
│
├── Services
│   ├── BaseService.cs        # Provides common CRUD and DTO mapping logic
│   ├── MainServices.cs       # Example of higher-level service
│   ├── SrvResponse.cs        # Standardized service response model
│   │
│   ├── DTO
│   │   └── DtoBase.cs        # Base class for Data Transfer Objects
│   │
│   ├── Interface             # Service contracts
│   │   ├── IMainServices.cs
│   │   └── IUsersBaseService.cs
│   │
│   ├── Mapper                # AutoMapper integration
│   │   ├── AutoMapperService.cs
│   │   ├── MapConfig.cs
│   │   └── MappingExtensions.cs
│   │
│   ├── Places                # Services for location management
│   │   ├── Srv_Countries.cs
│   │   ├── Srv_Cities.cs
│   │   └── Srv_Regions.cs
│   │
│   └── Users                 # Services for user and role management
│       ├── GSrv_Users.cs
│       └── UsersExt.cs
│
├── Utils
│   ├── Emails
│   │   ├── IEmailServices.cs # Email service contract
│   │   ├── EmailMessage.cs
│   │   ├── EmailResponse.cs
│   │   ├── SmtpEmailSender.cs
│   │   └── SmtpSettings.cs
│   │
│   ├── Enums                 # System-wide enums
│   │   ├── FieldTypes.cs
│   │   ├── Gender.cs
│   │   ├── RequestType.cs
│   │   ├── ResponseCode.cs
│   │   └── VisitStatus.cs
│   │
│   ├── SelectListHelper.cs   # Helpers for dropdowns and lists
│   └── SetEntitesOpData      # Utilities for setting default entity operation data
│
└── ViewModels
    └── BasePage.cs           # Base model for paginated responses

```
---



# 🛢️ DataBase : 

### 📘 Explanation of Db_BaseContext


The Db_BaseContext<`TUser`, `TRole`, `TUserRole`> is a generic Entity Framework Core DbContext that extends the built-in IdentityDbContext.
It is designed to work with customized ASP.NET Core Identity models, allowing flexibility when defining your own User, Role, and UserRole classes.

🔹 Generic Version : 
    
    public class Db_BaseContext<TUser, TRole, TUserRole>:
            IdentityDbContext<TUser, TRole, string,
                      IdentityUserClaim<string>,
                      TUserRole,
                      IdentityUserLogin<string>,
                      IdentityRoleClaim<string>,
                      IdentityUserToken<string>>
    where TUser : IdentityUser
    where TRole : IdentityRole
    where TUserRole : IdentityUserRole<string>

✅ Key Points:

* Inherits from IdentityDbContext with a string as the primary key type.
* The generics (TUser, TRole, TUserRole) allow using custom Identity classes instead of the defaults.
* Provides flexibility for extending user, role, and relationship entities.

✅ Constructors

* Both constructors allow dependency injection of DbContextOptions.
* Supports configuration from the application’s startup (connection strings, providers, etc.).

      public Db_BaseContext(DbContextOptions options) : base(options) { }
      public Db_BaseContext(DbContextOptions<Db_BaseContext<TUser, TRole, TUserRole>> options) : base(options) { }



✅ OnModelCreating
    
    protected override void OnModelCreating(ModelBuilder builder)
        {
             base.OnModelCreating(builder);

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
        }


* Overrides the default EF Core model building.
* Configures many-to-many relationships between User and Role through UserRole.
* Ensures:
  * Composite key on (UserId, RoleId).
  * Navigation properties (UserRoles) are properly mapped.


✅DbSets
  
    public DbSet<tbOTP> tbOTP { get; set; }
    public DbSet<tbCity> tbCity { get; set; }
    public DbSet<tbRegion> tbRegion { get; set; }
    public DbSet<tbCountry> tbCountry { get; set; }

        

* These represent additional application-specific tables:
  * tbOTP → Stores One-Time Passwords for authentication.
  * tbCity, tbRegion, tbCountry → Store geographical data.

----------------------------------------------------------------
🔹 Non-Generic Version

    public class Db_BaseContext : Db_BaseContext<AppUser, AppRole, UserRole>{
	    public Db_BaseContext(DbContextOptions options) : base(options) { }
	    public Db_BaseContext(DbContextOptions<Db_BaseContext> options) : base(options) { }
    }

* This is a concrete implementation of the generic context.
* Uses specific entity classes:
  * AppUser → Custom user entity.
  * AppRole → Custom role entity.
  * UserRole → Custom join entity for user-role relations.
* Allows using Db_BaseContext directly without specifying generic parameters.

✅ Summary

* Db_BaseContext<TUser, TRole, TUserRole> is a reusable base DbContext for ASP.NET Core Identity.
* Handles both Identity tables (users, roles, claims, logins, etc.) and custom tables (OTP, City, Region, Country).
* Provides generic flexibility for custom Identity implementations, and a ready-to-use non-generic version for standard cases.

------------------------------------------------------------------------
# Entities Layer Overview

The **Entities Layer** provides a set of pre-built models and contracts that developers can directly use or extend when building applications.  
It is designed to cover **core identity, user management, location data, and shared entity behaviors**.

---

## 📂 Contracts
- **IAppUser**  
  Defines the base contract for application users. It allows developers to extend their own user entity while maintaining consistency across the system.

- **ISoftEntity**  
  Provides a contract for entities that support **soft delete**. Instead of removing records from the database, entities can be marked as deleted while still being stored.

---

## 📂 Places
Entities representing **geographical data** that can be reused in any system needing location references.

- **tbCity** → Represents a city.  
- **tbCountry** → Represents a country.  
- **tbRegion** → Represents a region (linked to a country and possibly to cities).

These are useful for applications needing **address management, regional settings, or location-based filtering**.

---

## 📂 Users
Entities related to **user and role management**, fully compatible with ASP.NET Core Identity.

- **Const › Cnst_Roles**  
  Provides predefined constants for roles (e.g., Admin, User, Manager). Helps keep role names consistent across the system.

- **AppRole**  
  Extends the ASP.NET Core `IdentityRole`, allowing you to add custom role-related properties.

- **AppUser**  
  Extends the ASP.NET Core `IdentityUser`, allowing you to add custom user-related properties (e.g., FirstName, LastName).

- **UserRole**  
  Defines the relationship between users and roles (many-to-many). Extends `IdentityUserRole<string>`.

- **tbOTP**  
  Represents a One-Time Password (OTP) entity for **multi-factor authentication, email/SMS verification, or secure actions**.

---

## 📂 Base Entities
- **BaseEntity**  
  A base class for entities with common properties like:  
  - `Id` (int)  
  - `CreatedDate`  
  - `UpdatedDate`  
  - `IsDeleted` (if combined with ISoftEntity)

- **GuidBaseEntity**  
  Similar to `BaseEntity`, but uses a **GUID** as the primary key instead of an integer.  
  Useful for distributed systems or when a globally unique identifier is required.

---

## 🎯 Benefits
- **Reusability**: Developers can use the provided entities directly or extend them.  
- **Consistency**: Shared contracts and base classes ensure uniform patterns across projects.  
- **Identity-ready**: Full integration with ASP.NET Core Identity.  
- **Extendable**: Flexible enough to allow customization for different project needs.


---

## 🚀 Usage Example

### 1. Extending the `AppUser`
You can inherit from `AppUser` to add custom user-related properties such as `FirstName` and `LastName`.
    
    public class MyUser : AppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }




### 2. Creating an Entity from `BaseEntity`
For business-related tables, you can inherit from `BaseEntity` to reuse common fields (`Id`, `CreatedDate`, `UpdatedDate`).

    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }



### 3. Creating an Entity from `GuidBaseEntity`
For business-related tables, you can inherit from `GuidBaseEntity` to reuse common fields (`Id`, `CreatedDate`, `UpdatedDate`).

    public class Product : GuidBaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }


### 4. Using `ISoftEntity` for Soft Delete
If you want soft delete support, implement `ISoftEntity`.

    public class Customer : BaseEntity, ISoftEntity
    {
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }

-------------------------------------------------------------------------------

# DAl Layer Overview

# IRepository<TEntity>

The `IRepository<TEntity>` interface is a **generic repository contract** that provides a consistent way to perform **CRUD operations**, **queries**, and **data access** for any entity in the system.  

It abstracts the database layer (via **Entity Framework Core DbSet<TEntity>**) and ensures a clean separation of concerns between the **Data Access Layer (DAL)** and the **Business Layer**.

---

## 🧩 Key Features
- Works with **any entity** (`TEntity`) that is a class.  
- Provides **CRUD operations** (`Add`, `Remove`, `Update`).  
- Supports **advanced querying** with LINQ expressions.  
- Allows **pagination**, **sorting**, and **eager loading** (`Include`).  
- Can execute **raw SQL queries** when needed.  

---

## 🔑 Members

### 📦 DbSet
- `DbSet<TEntity> _db { get; }`  
  Exposes the underlying EF Core `DbSet<TEntity>` for direct database operations.

---

### ➕ Add Operations
- `void Add(TEntity entity)` → Add a single entity.  
- `void AddRange(IEnumerable<TEntity> entities)` → Add multiple entities.

---

### ❌ Remove Operations
- `void Remove(int id)` → Remove an entity by integer ID.  
- `void Remove(TEntity entity)` → Remove an entity instance.  
- `void RemoveRange(IEnumerable<TEntity> entities)` → Remove multiple entities.

---

### ✏️ Update Operations
- `void Update(TEntity entity, bool Noattach = true)`  
  Updates an entity.  
  - `Noattach=true` prevents EF from attaching the entity if already tracked.

---

### 🔍 Get (Single Item)
- `TEntity Get(int id)`  
- `TEntity Get(string id)`  
- `TEntity Get(Guid id)`  
- `TEntity GetFirstOrDefault(predicate, includes)` → Fetches the first matching record.  
- `TEntity GetLastOrDefault(predicate, includes)` → Fetches the last matching record.  
- `TResult GetWithSelect(selector, predicate)` → Fetches a single projected result.

---

### 📋 Get All (Multiple Items)
- `IEnumerable<TEntity> GetAll()` → Returns all entities.  
- `IEnumerable<TEntity> GetAll(predicate, orderBy, includes)` → Filter, order, and include related entities.  
- `IEnumerable<TResult> GetAll(predicate, selector, orderBy, includes)` → Return projected results (DTOs, custom objects).

---

### 📄 Pagination
- `IEnumerable<TEntity> GetPage(page, pageSize, out count, predicate, orderBy, includes)`  
  Returns paginated entities.  
- `IEnumerable<TResult> GetPage(selector, predicate, page, pageSize, out count, orderBy, includes)`  
  Returns paginated and projected results.

---

### ⚡ Raw SQL
- `IQueryable<TEntity> GetFromSql(string sql, params object[] parameters)`  
  Execute raw SQL queries for special cases.

---

### 🔢 Count
- `int GetCount()` → Returns total count of entities.  
- `int GetCount(predicate)` → Returns filtered count.

---

## ✅ Benefits of Using IRepository
- **Reusability** → One repository works with all entities.  
- **Maintainability** → Keeps data access logic consistent.  
- **Flexibility** → Supports both LINQ queries and raw SQL.  
- **Testability** → Easy to mock in unit tests.  
- **Clean Architecture** → Business logic doesn’t directly depend on EF Core.

---

## 💡 Example Usage

```csharp
public class UserService
{
    private readonly IRepository<AppUser> _userRepo;

    public UserService(IRepository<AppUser> userRepo)
    {
        _userRepo = userRepo;
    }

    public IEnumerable<AppUser> GetActiveUsers()
    {
        return _userRepo.GetAll(u => u.IsActive);
    }

    public void AddUser(AppUser user)
    {
        _userRepo.Add(user);
    }

    public void DeleteUser(int id)
    {
        _userRepo.Remove(id);
    }
}
```
---

# UnitOfWork Overview

# 📘 IUnitOfWork Interface

## ✨ Overview
The `IUnitOfWork` interface represents the **Unit of Work** pattern.  
It acts as a contract for managing **Repositories** and committing changes to the database in a consistent and organized way.

---

## 🔑 Code
```csharp
public interface IUnitOfWork : IDisposable
{
    // Get repository for a specific entity
    IRepository<T> repo<T>() where T : class;

    // Commit changes synchronously
    UnitOfWorkResult Save();

    // Commit changes asynchronously
    Task<UnitOfWorkResult> Saveasync();
}

```

## 🧩 Members Explained

1. IDisposable
Ensures proper release of resources (like the DbContext) after usage.
Any class implementing this interface must implement the Dispose() method.

2. IRepository<T> repo<T>() where T : class

A generic method that returns the repository for the specified entity.

Example:
```csharp
    var userRepo = unitOfWork.repo<User>();
    var productRepo = unitOfWork.repo<Product>();
```

3. `UnitOfWorkResult` Save()

* Commits all pending changes to the database.
* Returns a UnitOfWorkResult object that contains:
  * ✅ Success status
  * ❌ Error message if something goes wrong

#### 📝 UnitOfWorkResult

`UnitOfWorkResult` is a simple result wrapper class that is used together with the `IUnitOfWork` interface.  
It provides feedback about the outcome of database operations such as `Save()` or `SaveAsync()`.

##### ✨ Properties
- **`IsCompleted`** *(bool)*  
  Indicates whether the operation was successful (`true`) or failed (`false`).

- **`Message`** *(string)*  
  Holds an optional message, usually describing an error or additional information about the operation.

- **`innerObject`** *(object)*  
  Can store the exception object or any additional details related to the failure.

##### 🔗 Relation with IUnitOfWork
   When calling methods like:
    
```csharp
var result = unitOfWork.Save();```---    
4. Task<UnitOfWorkResult> Saveasync()

* Asynchronous version of Save().
* Useful for high-load applications where async improves responsiveness and scalability.

Example:
    
    var result = await unitOfWork.Saveasync();


---

# 📦 Services Layer
## Overview
The Services Layer provides a centralized place for implementing business logic and application workflows.
It acts as a bridge between the data access layer (repositories & UnitOfWork) and the presentation layer (controllers, APIs, UI).


### BaseService
The `BaseService<TEntity, TDto,TKey>` is a **generic service class** that provides common CRUD operations and DTO mapping for any entity type.

    public class BaseService<TEntity, TDto, TKey>
    where TEntity : class, iSoftEntity<TKey>, new()
    where TDto : class

#### Purpose
The BaseService class provides a generic service layer that simplifies CRUD (Create, Read, Update, Delete) operations.

##### It works with:

* Entities (TEntity)
* Data Transfer Objects (DTOs) (TDto)
* Primary Key type (TKey)

##### Key Features

* Abstracts repetitive CRUD logic.
* Uses IUnitOfWork + IRepository<TEntity> for data access.
* Integrates with AutoMapper (or custom mapping extensions).
* Returns responses wrapped in a consistent SrvResponse.

##### Main Methods

* GetItem(Guid id) → Retrieve a single entity and map to DTO.
* DeleteItem(Guid id, string userId) → Soft-delete or remove entity safely.
* SaveItem(TDto dto, string userId) → Add or update entity with audit info.
* GetAll() → Retrieve all non-deleted items.
* GetList(int PageIndex, int PageSize) → Paginated query.

---
### SrvResponse

## 📦 SrvResponse

The `SrvResponse` class is a **standardized response wrapper** used across all services.  
It ensures that service methods return **consistent results** regardless of success or failure, making error handling and data passing easier.

---

### 🔑 Key Features
- **Unified Response Format** → Encapsulates data, messages, and status codes in one object.  
- **Error & Success Handling** → Easily differentiate between successful and failed operations using the `IsOk` property.  
- **Flexible Constructors** → Supports multiple ways to initialize a response (with data, message, or status code).  
- **Additional Metadata** → Allows attaching controller/action names, property names, or any extra object.  

---

### 🛠️ Properties

| Property       | Type          | Description                                                                 |
|----------------|--------------|-----------------------------------------------------------------------------|
| `IsOk`         | `bool`       | Indicates whether the response is successful (`ResponseCode.OK`).          |
| `_ResponseCode`| `ResponseCode`| Status code of the operation (e.g., OK, InternalServerError, BadRequest).  |
| `Message`      | `string`     | A message describing the result (error or success).                         |
| `Controller`   | `string`     | (Optional) Name of the controller handling the request.                     |
| `Actions`      | `string`     | (Optional) Name of the action/method handling the request.                  |
| `Data`         | `object`     | Main data returned by the operation (e.g., DTO, entity, list).              |
| `AdditionalData` | `object`   | Extra metadata (e.g., total count for paged results).                       |
| `PropertyName` | `string`     | (Optional) The related property name, often used for validation errors.     |
| `innerObject`  | `object`     | (Optional) Holds additional internal objects for debugging or context.      |

---

### 🚀 Usage Examples

```csharp
// Success response with data
var response = new SrvResponse(myDto);

// Error response with custom message
var response = new SrvResponse("An unexpected error occurred");

// Response with specific status code and message
var response = new SrvResponse(ResponseCode.BadRequest, "Invalid input provided");

// Response with property-specific error
var response = new SrvResponse(ResponseCode.BadRequest, "Email is required", "Email");

```

✅ Benefits

* Provides consistency across all service responses.
* Makes it easier to handle errors in a unified way.
* Supports flexible initialization for different scenarios.
* Works seamlessly with SrvResponseExt extension methods for Success, Error, and BadRequest.


## 🛠️ SrvResponseExt

The `SrvResponseExt` class provides a set of **extension methods** for the `SrvResponse` object.  
It simplifies creating **success**, **error**, and **bad request** responses, and adds utility methods to **retrieve data safely**.

---

### 🔑 Key Features
- **Fluent API** → Build responses in a clear and consistent way.  
- **Error Handling** → Quickly generate error responses with messages and status codes.  
- **Data Extraction** → Safely retrieve items, lists, or counts from a response.  
- **Null Safety** → Ensures that a valid `SrvResponse` is always returned, even if the original object is null.  

---

### 🛠️ Main Methods

| Method                                   | Description                                                                 |
|------------------------------------------|-----------------------------------------------------------------------------|
| `Success()`                              | Returns a successful response (with optional data and additional metadata). |
| `Error(ResponseCode, string)`            | Returns an error response with a custom code and message.                   |
| `Error(string, object?)`                 | Returns an error response with a message and optional inner object.         |
| `BadRequest(string)`                     | Returns a bad request response with a message.                              |
| `_Return(ResponseCode, string, string?)` | Returns a response with custom code, message, and optional property name.   |
| `IsOk()`                                 | Checks if the response status is `OK`.                                      |
| `GetItem<T>()`                           | Retrieves a single object if the response is successful.                    |
| `GetItems<T>()`                          | Retrieves a list of objects if the response is successful.                  |
| `GetItemsCount()`                        | Retrieves a count from `AdditionalData` if the response is successful.      |

---

### 🚀 Usage Examples

```csharp
// Success with no data
var response = new SrvResponse().Success();

// Success with data
var response = new SrvResponse().Success(myDto);

// Success with data and count
var response = new SrvResponse().Success(myList, totalCount);

// Error with custom code
var response = new SrvResponse().Error(ResponseCode.NotFound, "Item not found");

// Error with default InternalServerError
var response = new SrvResponse().Error("Unexpected error occurred");

// Get a single item safely
var item = response.GetItem<MyDto>();

// Get a list of items safely
var items = response.GetItems<MyDto>();

// Get item with error message output
var item = response.GetItem<MyDto>(out string error);

```

✅ Benefits

Reduces repetitive boilerplate code when handling service responses.

Makes service results predictable and consistent across the application.

Provides safe casting for data retrieval.

Improves readability with a fluent API style.

---
# Service Aggregator


## IMainServices

### Description

`IMainServices` is a lightweight
service-aggregator interface that exposes domain services (Countries, Cities, Regions) as read-only properties.
It allows consumers (controllers, other services) to depend on a single entry point instead of injecting multiple domain services individually.

    public interface IMainServices
    {
        Srv_Countries _SrvCountries { get; }
        Srv_Cities _SrvCities { get; }
        Srv_Regions _SrvRegions { get; }
    }   

---

### ✅ Properties

* _SrvCountries — Access to Srv_Countries (country-related business logic).
* _SrvCities — Access to Srv_Cities (city-related business logic).
* _SrvRegions — Access to Srv_Regions (region-related business logic).

Each property returns a service that encapsulates domain operations (CRUD, validation, queries) for its domain.


### ⚙️ Usage
#### You Can Inhert From It 

```csharp
    public interface IMyMainService:IMainServices
    {
        // Add more domain services if needed
	    Srv_class _Srvclass { get; }
    }
    public class MyMainService : MainServices, IMyMainService
    {

    }
```
----
#### Use is

    // DI registration (example)
    services.AddScoped<IMyMainService, MyMainServices>();

    // Inject and use in a controller
    public class CountryController : ControllerBase
    {
        private readonly IMyMainService _mainServices;

        public CountryController(IMainServices mainServices)
        {
            _mainServices = mainServices;
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            var response = _mainServices._SrvCountries.GetAll();
            return Ok(response);
        }
    }
---
Note: MainServices usually implements lazy initialization (creates each Srv_* only when first accessed), so injecting IMainServices is lightweight.


### ✨ Benefits

* Simpler DI — inject one interface instead of many services.
* Cleaner constructors — reduces parameter clutter in controllers/services.
* Scalable — easy to add new domain services to the aggregator.
* Consistent access — centralizes how domain services are retrieved.

---
# Identity User Management Service
## 🛠️ IUsersBaseService<TUser, TRole, TdtoUser>

The `IUsersBaseService` interface defines a **generic contract** for managing users, roles, and authentication in an ASP.NET Core Identity-based application.  
It centralizes all common **user operations** (create, update, authentication, password management, roles, etc.) while keeping them strongly typed with generics.

---

### 🔑 Key Features
- **User Management** → Create, update, and fetch users by ID or username.  
- **Authentication** → Sign in, sign out, and confirm emails.  
- **Roles Handling** → Retrieve user roles or update them dynamically.  
- **Password Control** → Change or force reset passwords.  
- **DTO Integration** → Built-in mapping between entities and DTOs using `IMapper`.  
- **Standardized Responses** → Uses `SrvResponse` for consistent return values.  

---

### 🛠️ Main Methods

| Method                                   | Description                                                                 |
|------------------------------------------|-----------------------------------------------------------------------------|
| `EmailConfirmation(string)`              | Confirms a user's email address.                                            |
| `GetUser(ClaimsPrincipal)`               | Retrieves the logged-in user from claims.                                   |
| `GetUser(string)`                        | Retrieves a user by username.                                               |
| `GetUserByID(string)`                    | Retrieves a user by unique ID.                                              |
| `GetRoles(string)`                       | Returns a list of roles for the given user ID.                              |
| `GetRoles(ClaimsPrincipal)`              | Returns roles for the current logged-in user.                               |
| `CreateUser(TdtoUser)`                   | Creates a new user from a DTO.                                              |
| `GeTdtoUserById(string)`                 | Retrieves a DTO representation of a user by ID.                             |
| `GeTdtoUserByUserName(string)`           | Retrieves a DTO representation of a user by username.                       |
| `SignInAsync(DtoUserLogin)`              | Signs in a user with login credentials.                                     |
| `SignOut()`                              | Signs out the current user.                                                 |
| `updateUser(TdtoUser)`                   | Updates user details.                                                       |
| `GetUsres(string, int, int)`             | Retrieves paginated user results with optional search text.                 |
| `ChangePassword(Dto_ChangePassword)`     | Changes the password for the current user.                                  |
| `ForceChangePassword(string, string)`    | Forces a password reset for a specific user.                                |
| `updateUserRole(string, string, string)` | Updates the role of a specific user.                                        |

---

### 🚀 Usage Example

```csharp
public class UserController : ControllerBase
{
    private readonly IUsersBaseService<AppUser, IdentityRole, DtoUser> _userService;

    public UserController(IUsersBaseService<AppUser, IdentityRole, DtoUser> userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(DtoUser model)
    {
        var result = await _userService.CreateUser(model);
        if (result.IsOk)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }
}
```

### ✅ Benefits


* Centralized user management with a single reusable contract.
* Works seamlessly with ASP.NET Core Identity.
* Reduces controller boilerplate by pushing logic to the service layer.
* Strongly typed generics allow customization for different user, role, and DTO implementations.
* Unified error/success handling using SrvResponse.

---

## AutoMapper Integration

### Overview
The `AutoMapper` integration in the Services Layer provides a seamless way to map between **Entities** and **Data Transfer Objects (DTOs)**.
It leverages the popular AutoMapper library to reduce boilerplate code and ensure consistent mapping logic across the application.


## 🛠️ AutoMapperService

The `AutoMapperService` class provides a **centralized static access point** for the `IMapper` instance.  
It ensures that mapping operations across the application always use a single, consistent AutoMapper configuration.

---

### 🔑 Key Features
- **Global Mapper Access** → Exposes a static `Mapper` property accessible anywhere in the application.  
- **Centralized Initialization** → Ensures `IMapper` is initialized once and reused everywhere.  
- **Integration with Extensions** → Works seamlessly with `MappingExtensions` to perform object-to-object mapping.  

---

### 🛠️ Main Members

| Member                   | Description                                                                 |
|--------------------------|-----------------------------------------------------------------------------|
| `IMapper Mapper`         | A static property that holds the globally available AutoMapper instance.    |
| `Initialize(IMapper)`    | Initializes the global AutoMapper instance with the provided configuration. |

---

### 🚀 Usage Examples

```csharp
// Initialize AutoMapper (e.g., during app startup)
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDto>();
});

AutoMapperService.Initialize(mapperConfig.CreateMapper());

or if Using mapperCongig from MapConfig.cs
var mapper = app.Services.GetRequiredService<IMapper>();
AutoMapperService.Initialize(mapper);


// Access the global mapper anywhere
var dto = AutoMapperService.Mapper.Map<UserDto>(userEntity);


```

✅ Benefits

* Provides a single source of truth for AutoMapper usage.
* Simplifies dependency management by avoiding repeated mapper injection.
* Works as the backbone for mapping utilities like MappingExtensions.

---


## 🛠️ MappingExtensions

The `MappingExtensions` class provides **extension methods** for mapping objects and collections using **AutoMapper**.  
It simplifies object-to-object mapping by offering reusable methods for single items and lists.

---

### 🔑 Key Features
- **Single Item Mapping** → Convert any object to a specified destination type.  
- **List Mapping** → Convert a collection of objects into a list of the destination type.  
- **Validation** → Throws an exception if the source object or list is `null`.  
- **AutoMapper Integration** → Uses a centralized `AutoMapperService.Mapper` instance.  

---

### 🛠️ Main Methods

| Method                                | Description                                                                 |
|---------------------------------------|-----------------------------------------------------------------------------|
| `MapItem<TDestination>(this object)`  | Maps a single object to the target type `TDestination`.                     |
| `MapList<TDestination>(this IEnumerable<object>)` | Maps a collection of objects to a list of `TDestination`.                   |

---

### 🚀 Usage Examples

```csharp
// Map a single object
var dto = userEntity.MapItem<UserDto>();

// Map a list of objects
var dtoList = userEntities.MapList<UserDto>();

```
✅ Benefits

* Reduces boilerplate mapping code.
* Makes use of AutoMapper in a clean and reusable way.
* Provides null-safety checks to avoid runtime issues.
* Enhances code readability and maintainability.

