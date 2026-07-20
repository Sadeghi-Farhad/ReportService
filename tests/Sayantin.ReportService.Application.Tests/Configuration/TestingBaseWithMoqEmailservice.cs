using ReportService.Domain.Interfaces;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit.Abstractions;

namespace ReportService.Application.Tests.Configuration;

public class TestingBaseWithMoqEmailService : IClassFixture<TestingWebAppFactory>, IDisposable
{
    private readonly IServiceScope Scope;
    protected ISender Sender { get; }
    protected ITestOutputHelper OutputHelper { get; }

    public TestingBaseWithMoqEmailService(TestingWebAppFactory factory, ITestOutputHelper outputHelper)
    {
        OutputHelper = outputHelper;

        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(es => es.SendEmailAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns(() =>
            {
                OutputHelper.WriteLine("Mock_Email_Service");
                return Task.CompletedTask;
            });

        var customFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IEmailService>();
                services.AddScoped(_ => emailServiceMock.Object);
            });
        });

        Scope = customFactory.Services.CreateScope();
        Sender = Scope.ServiceProvider.GetRequiredService<ISender>();
    }

    public void Dispose() => Scope.Dispose();
}