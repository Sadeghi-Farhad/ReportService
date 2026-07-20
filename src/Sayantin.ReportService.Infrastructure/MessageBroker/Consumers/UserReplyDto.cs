namespace ReportService.Infrastructure.MessageBroker.Consumers
{
    public class UserReplyDto
    {
        public int RetryCount { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
    }
}
