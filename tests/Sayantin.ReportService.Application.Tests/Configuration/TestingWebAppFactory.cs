using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ReportService.Application.Tests.Configuration;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(UseTestDbContext);
    }

    private void UseTestDbContext(IServiceCollection services)
    {
        // Remove Original Data Base Context
        services.RemoveAll(typeof(DbContextOptions<EFDbContext>));
        services.RemoveAll(typeof(EFDbContext));

        // Add InMemory Test Data Base Context
        var inMemoryProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

        services.AddDbContext<EFDbContext>(dbContextOptionsBuilder =>
            dbContextOptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(inMemoryProvider));
    }
}