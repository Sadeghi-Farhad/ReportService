namespace ReportService.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string from, string subject, string body);
    }
}
