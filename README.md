ساختار Solution
===============

```
Sayantin.ReportService.sln

src
│
├── Sayantin.ReportService.Api
│
├── Sayantin.ReportService.Application
│
├── Sayantin.ReportService.Domain
│
├── Sayantin.ReportService.Infrastructure
│
└── Sayantin.ReportService.Contracts

tests
│
└── Sayantin.ReportService.Tests
```

* * * * *

مسئولیت هر پروژه
================

Api
---

فقط

-   Controller
-   Authentication
-   Swagger
-   Middleware
-   DI
-   Program.cs

هیچ منطقی داخلش نیست.

* * * * *

Application
-----------

تمام Business Logic

مثلاً

```
GenerateEmbedTokenCommand

GenerateEmbedTokenHandler

GetReportsQuery

RefreshDatasetCommand
```

اگر در آینده MediatR اضافه کردی هم آماده است.

* * * * *

Domain
------

فقط Entity

مثلاً

```
Report

Workspace

Tenant

ReportPermission
```

بدون وابستگی.

* * * * *

Infrastructure
--------------

تمام ارتباطات خارجی

```
Power BI

Azure AD

Redis

SQL Server

Serilog
```

* * * * *

Contracts
---------

DTOها

```
EmbedRequest

EmbedResponse

ReportDto

WorkspaceDto
```

این پروژه را سرویس‌های دیگر هم می‌توانند Reference کنند.

فولدر Api
=========

```
Api

Controllers

Authentication

Middlewares

Extensions

Program.cs
```

* * * * *

فولدر Infrastructure
====================

```
Infrastructure

PowerBI

Azure

Repositories

Caching

Persistence

Logging
```

* * * * *

کلاس‌ها
=======

```
PowerBiClient

AzureTokenProvider

EmbedTokenService

PowerBiRepository

RedisCacheService
```

* * * * *

APIها
=====

```
GET

/api/reports
```

لیست گزارش‌ها

* * * * *

```
GET

/api/reports/{code}
```

گرفتن اطلاعات گزارش

* * * * *

```
POST

/api/reports/embed
```

ساخت Embed Token

* * * * *

```
POST

/api/reports/export/pdf
```

Export PDF

* * * * *

```
POST

/api/reports/refresh
```

Refresh Dataset

* * * * *

```
GET

/api/reports/status
```

Refresh Status

* * * * *

JWT
===

ReportService فقط Claimها را می‌خواند.

مثلاً

```
{
    "sub":"1",

    "companyId":"15",

    "branchId":"4",

    "roles":[
        "Accounting",
        "Manager"
    ]
}
```

* * * * *

RLS
===

هنگام Generate Token

```
CompanyId

BranchId

Department

UserId
```

به Power BI ارسال می‌شود.

* * * * *

Redis
=====

فقط Access Token

Cache می‌شود.

Embed Token

Cache نمی‌شود.

* * * * *

SQL
===

جدول Report

```
Reports

---------

Id

Code

Title

WorkspaceId

ReportId

DatasetId

Active
```

* * * * *

جدول Permission

```
ReportPermissions

-----------------

ReportId

RoleName
```

* * * * *

جدول Tenant

```
Tenants

------------

Id

CompanyId

WorkspaceId
```

* * * * *

تنظیمات
=======

به جای appsettings

من Options Pattern استفاده می‌کنم.

مثلاً

```
AzureOptions

PowerBIOptions

RedisOptions

JwtOptions
```

* * * * *

Logging
=======

Serilog

```
Console

Seq

File
```

* * * * *

Exception
=========

Middleware

```
ProblemDetails
```

* * * * *

HealthCheck
===========

```
/health
```

بررسی

-   SQL
-   Redis
-   Power BI
-   Azure AD

* * * * *

Docker
======

```
Dockerfile

docker-compose

Redis

Seq
```

* * * * *

CI/CD
=====

GitHub Actions

Build

Test

Docker Publish






















فولدر Api
=========

```
Api

Controllers

Authentication

Middlewares

Extensions

Program.cs
```

* * * * *

فولدر Infrastructure
====================

```
Infrastructure

PowerBI

Azure

Repositories

Caching

Persistence

Logging
```

* * * * *

کلاس‌ها
=======

```
PowerBiClient

AzureTokenProvider

EmbedTokenService

PowerBiRepository

RedisCacheService
```

* * * * *

APIها
=====

```
GET

/api/reports
```

لیست گزارش‌ها

* * * * *

```
GET

/api/reports/{code}
```

گرفتن اطلاعات گزارش

* * * * *

```
POST

/api/reports/embed
```

ساخت Embed Token

* * * * *

```
POST

/api/reports/export/pdf
```

Export PDF

* * * * *

```
POST

/api/reports/refresh
```

Refresh Dataset

* * * * *

```
GET

/api/reports/status
```

Refresh Status

* * * * *

JWT
===

ReportService فقط Claimها را می‌خواند.

مثلاً

```
{
    "sub":"1",

    "companyId":"15",

    "branchId":"4",

    "roles":[
        "Accounting",
        "Manager"
    ]
}
```

* * * * *

RLS
===

هنگام Generate Token

```
CompanyId

BranchId

Department

UserId
```

به Power BI ارسال می‌شود.

* * * * *

Redis
=====

فقط Access Token

Cache می‌شود.

Embed Token

Cache نمی‌شود.

* * * * *

SQL
===

جدول Report

```
Reports

---------

Id

Code

Title

WorkspaceId

ReportId

DatasetId

Active
```

* * * * *

جدول Permission

```
ReportPermissions

-----------------

ReportId

RoleName
```

* * * * *

جدول Tenant

```
Tenants

------------

Id

CompanyId

WorkspaceId
```

* * * * *

تنظیمات
=======

به جای appsettings

من Options Pattern استفاده می‌کنم.

مثلاً

```
AzureOptions

PowerBIOptions

RedisOptions

JwtOptions
```

* * * * *

Logging
=======

Serilog

```
Console

Seq

File
```

* * * * *

Exception
=========

Middleware

```
ProblemDetails
```

* * * * *

HealthCheck
===========

```
/health
```

بررسی

-   SQL
-   Redis
-   Power BI
-   Azure AD

* * * * *

Docker
======

```
Dockerfile

docker-compose

Redis

Seq
```

* * * * *

CI/CD
=====

GitHub Actions

Build

Test

Docker Publishبا توجه به تمام صحبت‌هایی که داشتیم (معماری میکروسرویس، حذف Contracts، استفاده از Controller، Clean Architecture، .NET 10، Power BI، Identity Service جداگانه)، اگر جای من بود، ساختار پروژه را به این صورت طراحی می‌کردم.

این ساختار را طوری چیده‌ام که تا چند سال آینده هم قابل توسعه باشد.

```
ReportService
│
├── docker
│   ├── Dockerfile
│   ├── docker-compose.yml
│   └── docker-compose.override.yml
│
├── docs
│   ├── Architecture.md
│   ├── API.md
│   └── Database.md
│
├── src
│   │
│   ├── ReportService.Api
│   │   │
│   │   ├── Controllers
│   │   │   ├── ReportsController.cs
│   │   │   ├── CategoriesController.cs
│   │   │   ├── DatasetsController.cs
│   │   │   ├── WorkspacesController.cs
│   │   │   └── HealthController.cs
│   │   │
│   │   ├── Requests
│   │   │   ├── Reports
│   │   │   │   ├── CreateReportRequest.cs
│   │   │   │   ├── UpdateReportRequest.cs
│   │   │   │   └── GenerateEmbedTokenRequest.cs
│   │   │   │
│   │   │   └── Categories
│   │   │       ├── CreateCategoryRequest.cs
│   │   │       └── UpdateCategoryRequest.cs
│   │   │
│   │   ├── Responses
│   │   │   ├── ErrorResponse.cs
│   │   │   └── ApiResponse.cs
│   │   │
│   │   ├── Middlewares
│   │   │   ├── ExceptionMiddleware.cs
│   │   │   ├── LoggingMiddleware.cs
│   │   │   └── CorrelationIdMiddleware.cs
│   │   │
│   │   ├── Extensions
│   │   │   ├── SwaggerExtensions.cs
│   │   │   ├── AuthenticationExtensions.cs
│   │   │   ├── AuthorizationExtensions.cs
│   │   │   ├── HealthCheckExtensions.cs
│   │   │   └── EndpointExtensions.cs
│   │   │
│   │   ├── Filters
│   │   │   └── ValidationFilter.cs
│   │   │
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   ├── Program.cs
│   │   └── ReportService.Api.csproj
│   │
│   │
│   ├── ReportService.Application
│   │   │
│   │   ├── Interfaces
│   │   │   ├── IReportService.cs
│   │   │   ├── ICategoryService.cs
│   │   │   ├── IReportRepository.cs
│   │   │   ├── ICategoryRepository.cs
│   │   │   ├── ICurrentUserService.cs
│   │   │   ├── IPowerBiReportClient.cs
│   │   │   ├── IPowerBiDatasetClient.cs
│   │   │   ├── IPowerBiWorkspaceClient.cs
│   │   │   └── IPowerBiExportClient.cs
│   │   │
│   │   ├── Services
│   │   │   ├── ReportService.cs
│   │   │   └── CategoryService.cs
│   │   │
│   │   ├── Models
│   │   │   ├── Reports
│   │   │   │   ├── ReportDto.cs
│   │   │   │   ├── CreateReportDto.cs
│   │   │   │   ├── UpdateReportDto.cs
│   │   │   │   └── EmbedTokenDto.cs
│   │   │   │
│   │   │   ├── Categories
│   │   │   │   └── CategoryDto.cs
│   │   │   │
│   │   │   ├── Security
│   │   │   │   ├── CurrentUser.cs
│   │   │   │   └── CustomClaimTypes.cs
│   │   │   │
│   │   │   └── PowerBI
│   │   │       ├── PowerBiReport.cs
│   │   │       ├── PowerBiDataset.cs
│   │   │       ├── PowerBiWorkspace.cs
│   │   │       ├── EmbedTokenResult.cs
│   │   │       ├── ExportStatus.cs
│   │   │       └── DatasetRefreshHistory.cs
│   │   │
│   │   ├── Validators
│   │   │   ├── CreateReportValidator.cs
│   │   │   ├── UpdateReportValidator.cs
│   │   │   └── CategoryValidator.cs
│   │   │
│   │   └── DependencyInjection.cs
│   │
│   │
│   ├── ReportService.Domain
│   │   │
│   │   ├── Entities
│   │   │   ├── Report.cs
│   │   │   ├── ReportCategory.cs
│   │   │   ├── ReportPermission.cs
│   │   │   └── UserFavoriteReport.cs
│   │   │
│   │   ├── Enums
│   │   │
│   │   ├── Events
│   │   │
│   │   ├── Exceptions
│   │   │
│   │   └── Common
│   │       ├── BaseEntity.cs
│   │       ├── AuditableEntity.cs
│   │       └── IEntity.cs
│   │
│   │
│   └── ReportService.Infrastructure
│       │
│       ├── Persistence
│       │   ├── ReportDbContext.cs
│       │   │
│       │   ├── Configurations
│       │   │   ├── ReportConfiguration.cs
│       │   │   ├── ReportCategoryConfiguration.cs
│       │   │   ├── ReportPermissionConfiguration.cs
│       │   │   └── UserFavoriteReportConfiguration.cs
│       │   │
│       │   ├── Repositories
│       │   │   ├── ReportRepository.cs
│       │   │   └── CategoryRepository.cs
│       │   │
│       │   ├── Migrations
│       │   │
│       │   └── Seed
│       │       ├── SeedData.cs
│       │       └── SeedExtensions.cs
│       │
│       ├── Identity
│       │   └── CurrentUserService.cs
│       │
│       ├── PowerBI
│       │   ├── Options
│       │   │   └── PowerBiOptions.cs
│       │   │
│       │   ├── Reports
│       │   │   └── PowerBiReportClient.cs
│       │   │
│       │   ├── Datasets
│       │   │   └── PowerBiDatasetClient.cs
│       │   │
│       │   ├── Workspaces
│       │   │   └── PowerBiWorkspaceClient.cs
│       │   │
│       │   └── Exports
│       │       └── PowerBiExportClient.cs
│       │
│       └── DependencyInjection.cs
│
├── tests
│   ├── ReportService.UnitTests
│   ├── ReportService.IntegrationTests
│   └── ReportService.ApiTests
│
├── .editorconfig
├── .gitignore
├── Directory.Build.props
├── global.json
├── ReportService.sln
└── README.md
```

چند نکته که در طراحی نهایی عمداً اعمال کردم
-------------------------------------------

-   **پروژه `Contracts` حذف شده است.**
-   DTOهای HTTP داخل `Api` هستند.
-   DTOهای Business داخل `Application` هستند.
-   `CurrentUser` و `CustomClaimTypes` در `Application` قرار گرفته‌اند تا هم Serviceها و هم Infrastructure از آن‌ها استفاده کنند.
-   تنها لایه‌ای که با **Power BI REST API** صحبت می‌کند، `Infrastructure` است.
-   فقط `Api` به HTTP، Swagger و Middleware وابسته است.
-   فقط `Infrastructure` به EF Core و SQL Server وابسته است.
-   `Domain` هیچ وابستگی به ASP.NET Core، EF Core یا Power BI ندارد.
-   برای هر پروژه یک فایل `DependencyInjection.cs` در نظر گرفته شده تا `Program.cs` بسیار تمیز بماند.

به نظر من این ساختار برای پروژه‌ای در مقیاس ERP که قرار است به‌عنوان یک میکروسرویس گزارش‌گیری مبتنی بر Power BI استفاده شود، ساختاری تمیز، قابل توسعه و مناسب برای نگهداری بلندمدت است.
