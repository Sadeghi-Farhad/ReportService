using ReportService.Domain.Interfaces;

namespace ReportService.Api.Common
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public string? AccessToken =>
            httpContextAccessor.HttpContext?
                .Request?
                .Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", "")
                .Replace("bearer ", "");
    }
}
