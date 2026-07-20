<div dir="rtl">

# معرفی
این پروژه قالب و الگویی است برای ایجاد وب سرویس (Web API)ها بر پایه معماری تمیز (Clean Architecture).

معماری تمیز (Clean Architecture) ، یک معماری محبوب برای سازماندهی اپلیکیشن‌ها محسوب می‌شود. این معماری طرفداران و منتقدان خود را دارا است؛ اما درنهایت، این رویکرد، یکی از معماری‌های مناسب برای پروژه‌های بزرگ و سطح سازمانی تلقی می‌شود.

![معماری تمیز](https://robinanet.ir/wp-content/uploads/CleanArchitecture.jpg)



# ساختار پروژه
ساختار این پروژه با مطالعه بهترین نمونه های پیاده سازی معماری تمیز ایجاد شده است و ساختار شناخته شده و معروف از پیاده سازی معماری تمیز می باشد.
چهار پروژه/لایه با نامهای زیر در solution وجود دارند:
1.	Domain
2.	Application
3.	Infrastructure
4.	API

## Domain
هسته معماری تمیز، Domain نام دارد.آنچه معمولاً در پروژه Domain تعریف می‌کنید، قوانین اصلی مربوط به کسب و کار، Enumerations ،Value Object ها، Custom Exception و چنین مواردی است.

<div dir="ltr">
In Clean Architecture, the central focus should be on Entities and business rules.

In Domain-Driven Design, this is the Domain Model.

This project should contain all of your Entities, Value Objects, and business logic.

Entities that are related and should change together should be grouped into an Aggregate.

Entities should leverage encapsulation and should minimize public setters.

Entities can leverage Domain Events to communicate changes to other parts of the system.

Entities can define Specifications that can be used to query for them.

For mutable access, Entities should be accessed through a Repository interface.

Read-only ad hoc queries can use separate Query Services that don't use the Domain Model.
</div>

## Application
اساساً، لایه Domain مجوز ارجاع داشتن به هیچ کدام از لایه‌های بیرونی را ندارد و این موضوع، یک قانون مهم در معماری Clean محسوب می‌شود. در حالی که لایه Application ، امکان برقراری ارتباط با لایه Domain را دارد.

پروژه Application یک Orchestrator از سایر لایه‌ها و Use Case ها تلقی می‌شود. این یعنی در این لایه، ماژول‌های مختلف فراخوانی و مورد استفاده قرار می‌گیرند و هیچ منطقی مرتبط با کسب و کار تعریف نخواهد شد. همچنین، در این لایه می‌توانید Service های گوناگون را فراخوانی و به‌کار ببرید.

معمولاً برای ارتباط بین لایه Entry Point و Application از mediator استفاده می‌شود. Mediator یک Design Pattern است که به‌واسطه آن، Couple-less بودن لایه‌های مختلف پروژه تضمین خواهد شد.

در لایه Application می‌توان Use Case ها را ایجاد کرد. لازم است هر Use Case، به‌عنوان یک کلاس مستقلی پیاده‌سازی شود که از MediatR.RequestHandler<TRequest ,TResponse> ارث‌بری می‌کند. پارامتر TRequest نشان‌دهنده شی درخواستی‌ خاصی است که به Use Case ارسال و پارامتر TResponse نمایان‌گر شی پاسخی است که از Use Case برگردانده می‌شود.

به‌صورت کلی، Command به فرآیندی گفته می‌شود که طی آن، تغییری در وضعیت سیستم به‌وجود می‌آید. در مقابل، اگر بخواهیم از وضعیت یک سرویس یا سیستم مطلع شویم، Query استفاده می‌شود. لایه اپلیکیشن جایی هست که Command ها و کوئری‌ها پیاده‌سازی خواهند شد.

### قانون نام‌گذاری Command ها و کوئری‌ها
در صورتی که بخواهیم یک Command برای ایجاد کاربر داشته باشیم، ابتدای نام مربوطه، تسکی که قرار است انجام دهد (یعنی Create)، سپس نام فیچر (یعنی User) و درنهایت، عبارت Command آورده می‌شود. یعنی می شود CreateUserCommand.

## Infrastructure
در لایه infrastructure، تمرکز روی پیاده‌سازی‌های مربوط به دیتابیس و ارتباط با سرویس‌های خارجی و سخت افزار است. در واقع همه مواردی که در لایه های Domain و Application بصورت انتزاع در نظر گرفته شده اند، در این لایه با توجه به پایگاه داده و یا سخت افزار  انتخاب شده، پیاده سازی می شوند. 

<div dir="ltr">
In Clean Architecture, Infrastructure concerns are kept separate from the core business rules (or domain model in DDD).

The only project that should have code concerned with EF, Files, Email, Web Services, Azure/AWS/GCP, etc is Infrastructure.

Infrastructure should depend on Core (and, optionally, Use Cases) where abstractions (interfaces) exist.

Infrastructure classes implement interfaces found in the Core (Use Cases) project(s).

These implementations are wired up at startup using DI. In this case using `Microsoft.Extensions.DependencyInjection` and extension methods defined in each project.
</div>

## API
پروژه Api در اینجا بعنوان Entry Point می باشد.
منظور از Entry Point یا نقطه ورودی پروژه، جایی‌ است که درخواست‌ها در ابتدا به آن ارسال می‌شوند و سپس، از آن جا به سایر لایه‌ها هدایت خواهند شد. این ورودی می‌تواند یک WebApi یا یک Console Application باشد.



# نحوه استفاده

<div dir="ltr">

## Entity Framework Core tools reference - .NET Core CLI
### Installing the tools
dotnet ef can be installed as either a global or local tool. Most developers prefer installing dotnet ef as a global tool using the following command:

`dotnet tool install --global dotnet-ef`

Update the tool to the latest available version using the following command:
`dotnet tool update --global dotnet-ef`

Before you can use the tools on a specific project, you'll need to add the Microsoft.EntityFrameworkCore.Design package to it.

```dotnet add package Microsoft.EntityFrameworkCore.Design```

### Verify installation
Run the following commands to verify that EF Core CLI tools are correctly installed:
`dotnet ef`

The output from the command identifies the version of the tools in use:
![](https://devblogs.microsoft.com/dotnet/wp-content/uploads/sites/10/2021/11/dotnetef.png)

for more information about Entity Framework Core tools visit [Entity Framework Core tools reference - .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Adding Migration
To add a new migration, use the following command: 

`dotnet ef migrations add <MigrationName>`

Replace `MigrationName` with a descriptive name for your migration. 
- This command analyzes your data model and creates a new migration file in your project's Migrations folder.
- The migration file contains code to create, update, or remove database schema elements based on your data model changes.

### Applying Migrations
To apply the pending migrations to your database, use:

```
dotnet ef database update
```
This command executes the migration scripts generated in the migration files, updating your database schema to match your data model.

### Removing Migrations
To remove the last applied migration, use: 
```
dotnet ef migrations remove
```

When you have a solution with 2 projects API/WebApp and a DataAcess project you can pass in the options on the command line.
```
My_Solution
       |DataAccess_Project
       |-- DbContext.cs
       |WebApp_Project
       |-- Startup.cs
```
Change into the solution directory: 
```
CD My_Solution
```
```
dotnet ef migrations add InitialCreate --project DataAccess_Project --startup-project WebApp_Project
```
```
dotnet ef database update --project DataAccess_Project --startup-project WebApp_Project
```

for Crouse service template use:
```
dotnet ef migrations add Migration_14040412 --project src/CrouseServiceTemplate.Infrastructure --startup-project src/CrouseServiceTemplate.Api --output-dir Data\Migrations
```
```
dotnet ef database update --project src/CrouseServiceTemplate.Infrastructure --startup-project src/CrouseServiceTemplate.Api
```

</div>

# کتابخانه ها و پکیج های معروف و مفید برای معماری دامنه محور
- [ErrorOr](https://github.com/amantinband/error-or)
- [Throw](https://github.com/amantinband/throw)
- [StyleCop Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
- [Ardalis.Result](https://github.com/ardalis/Result)
- [Ardalis.SmartEnum](https://github.com/ardalis/SmartEnum)
- [Ardalis.Specification](https://github.com/ardalis/Specification)
- [Ardalis.SharedKernel](https://github.com/ardalis/Ardalis.SharedKernel)
- [Ardalis.GuardClauses](https://github.com/ardalis/GuardClauses)


# منابع
- [معماری تمیز (Clean Architecture) چیست ؟ ۵ مرحله راه اندازی آن](https://nikamooz.com/what-is-clean-architecture/)
- [پیاده سازی معماری Domain Driven Design و تست نویسی](https://nikamooz.com/implementation-of-domain-driven-design-architecture/)
- [آموزش کاربردی DDD in ASP.NET Core - قسمت دوم](https://vrgl.ir/2Zmyq)
- [Designing DDD aggregates](https://medium.com/@albert.llousas/designing-ddd-aggregates-db633f1caf88)



* 	این راهنما با  ==Markdown== تهیه شده است.
* برای آشنایی بیشتر با آن می توانید به سایت [markdownguide.org](https://www.markdownguide.org/cheat-sheet/) مراجعه نمایید.
 
</div>


