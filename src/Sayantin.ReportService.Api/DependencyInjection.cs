using System.Reflection;
using ReportService.Api.CI;
using ReportService.Api.Common;
using ReportService.Api.Middlewares;
using ReportService.Domain.Interfaces;
using Microsoft.OpenApi.Models;

namespace ReportService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ApplicationInfo applicationInfo)
        {
            services.AddControllers();
            services.AddOpenApi();

            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = applicationInfo.Title,
                    Version = applicationInfo.Version?.ToString(3),
                    Contact = new OpenApiContact
                    {
                        Name = applicationInfo.Authors,
                        Email = applicationInfo.SupportEmail
                    },
                    Description = "See the [README](/template/doc?file=README.md) for full documentation.<br/>"
                   + "See the [ChangeLog](/template/doc?file=ChangeLog.md) for release notes."
                   + applicationInfo.ToString()
                });


                // JWT Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "لطفا برای اجرای فرایند خود توکن را وارد نمائید",
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddScoped<ExceptionHandlerMiddleware>();

            return services;
        }
    }
}
