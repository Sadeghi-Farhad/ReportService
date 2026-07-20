using ReportService.Domain.Exceptions;
using ReportService.Domain.Interfaces;
using ReportService.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace ReportService.Infrastructure.Email
{
    public class EmailService(IUserContext _userContext, IOptions<ApiEndpointsOptions> options) : IEmailService
    {
        string token = _userContext.AccessToken ?? string.Empty;
        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            EmailRequest request = new EmailRequest()
            {
                AppId = 4660,
                ToEmails = [to],
                senderEmail = from,
                MailSubject = subject,
                HtmlBody = body,
                ProcessName = "ReportService"
            };

            ApiCallManager.ApiManager apicaller = new();
            var result = await apicaller.PostAsync<EmailRequest, string>(options.Value.GatewayAddress + "/email/email/sendemail", request, true, token);

            if (!result.IsSuccess)
                throw new BaseException($"Email Sending exception. EmailSubject={subject} To={to} Exception={result?.Problem?.Title ?? string.Empty} - {result?.Problem?.Detail ?? string.Empty}");
        }
    }

    internal class EmailRequest
    {
        public string MailSubject { get; set; }
        public string HtmlBody { get; set; }
        public List<string> ToEmails { get; set; }
        public List<string> CcEmails { get; set; } = [];
        public string senderEmail { get; set; } = "crousesystem@crouse.ir";
        public int AttachmentId { get; set; } = 0;
        public int AppId { get; set; }
        public string ProcessName { get; set; }
    }
}