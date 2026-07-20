using ReportService.Application.Audit.Projections;
using ReportService.Application.Audit.Projections.Formatting;
using ReportService.Application.Mapping;
using ReportService.Application.Pipeline;
using ReportService.Domain.Audit;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ReportService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            // AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            // MediatR
            Assembly?[] mediatRAssemblies =
              [
                Assembly.GetAssembly(typeof(Domain.DependencyInjection)),
                Assembly.GetAssembly(typeof(Application.DependencyInjection))
              ];

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(mediatRAssemblies);

                // Pipeline
                cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingBehavior<>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            });

            services.AddScoped<IAuditProjection, AuditProjection>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IAuditValueFormatter, AuditValueFormatter>();

            return services;
        }
    }
}