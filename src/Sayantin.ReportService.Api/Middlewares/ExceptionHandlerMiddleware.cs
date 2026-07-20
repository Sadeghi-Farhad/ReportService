using System.Net;
using System.Text.Json;
using ReportService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Middlewares
{
    public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"An exception occurred. Time={DateTime.Now} Type={ex.GetType()} Message= {ex.Message} Exception= {ex.ToString()}");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context?.Request?.Path ?? string.Empty,
                Type = exception.GetType()?.ToString() ?? string.Empty,
                Title = "خطای نامشخص",
                Detail = exception?.Message ?? string.Empty,
                Status = (int)HttpStatusCode.InternalServerError
            };

            switch (exception)
            {
                case Domain.Exceptions.KeyNotFoundException notFound:
                    problemDetails.Title = "یافت نشد";
                    problemDetails.Status = (int)HttpStatusCode.NoContent;
                    break;

                case Application.Exceptions.ValidationException validation:
                    problemDetails.Title = "اعتبارسنجی رد شد";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Extensions["errors"] = validation.Errors;
                    break;

                case BaseException baseException:
                    problemDetails.Title = "خطای برنامه";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    problemDetails.Title = "خطای نامشخص";
                    problemDetails.Detail = env.IsDevelopment() ? (exception?.StackTrace ?? "No details") : "-";
                    break;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status.Value;

            await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails, new JsonSerializerOptions
            {
                WriteIndented = env.IsDevelopment()
            });
        }
    }
}