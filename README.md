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
