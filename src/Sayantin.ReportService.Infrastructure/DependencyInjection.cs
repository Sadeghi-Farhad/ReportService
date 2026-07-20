using ApiCallManager;
using ReportService.Domain.Blogs;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Personnels;
using ReportService.Domain.Users;
using ReportService.Infrastructure.Audit;
using ReportService.Infrastructure.Configurations;
using ReportService.Infrastructure.Data.Configuration;
using ReportService.Infrastructure.Email;
using ReportService.Infrastructure.MessageBroker.Channel;
using ReportService.Infrastructure.MessageBroker.Consumers;
using ReportService.Infrastructure.MessageBroker.Producers;
using ReportService.Infrastructure.Personnels;
using ReportService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReportService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // AddDatabase    
            services.AddDbContext<EFDbContext>((sp, options) =>
            {
                var auditInterceptor = sp.GetRequiredService<AuditSaveChangesInterceptor>();

                options.UseSqlServer(configuration.GetConnectionString("TemplateDbContext"))
                       .AddInterceptors(auditInterceptor);
            });

            // AddUnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // AddRepositories
            services
                .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IBlogRepository, BlogRepository>()
                .AddScoped<IPersonnelInfo, PersonnelInfo>()
                .AddScoped<AuditSaveChangesInterceptor>()
                .AddScoped<IAuditCollector, AuditCollector>()
                .AddScoped<IAuditRepository, AuditRepository>()
                .AddScoped<IApiManager, ApiManager>();

            // Add RabbitMQ
            services
                .AddSingleton<IChannelFactory, ChannelFactory>()
                .AddHostedService<BlogReplyConsumer>()
                .AddHostedService<UserCreatedConsumerHostedService>()
                .AddScoped<IBlogPublishedProducer, BlogPublishedProducer>();

            // AddBusinessServices
            services.Configure<ApiEndpointsOptions>(configuration.GetSection("ApiEndpoints"));
            services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}