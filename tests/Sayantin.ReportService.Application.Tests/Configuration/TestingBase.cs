namespace ReportService.Application.Tests.Configuration
{
    public class TestingBase : IClassFixture<TestingWebAppFactory>, IDisposable
    {
        private readonly IServiceScope scope;
        protected ISender Sender { get; }

        public TestingBase(TestingWebAppFactory factory)
        {
            scope = factory.Services.CreateScope();
            Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}