using ReportService.Api;
using ReportService.Api.CI;
using ReportService.Api.Middlewares;
using ReportService.Application;
using ReportService.Domain;
using ReportService.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/ReportService.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services.AddPresentation(new ApplicationInfo(builder));
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddDomain();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportService");
    });

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}

public partial class Program
{
}