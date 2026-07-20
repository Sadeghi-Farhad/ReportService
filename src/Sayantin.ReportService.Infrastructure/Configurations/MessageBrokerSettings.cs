namespace ReportService.Infrastructure.Configurations
{
    public sealed class MessageBrokerSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }

        public string UserName { get; } = "ReportService";
        public string Password { get; } = "Template123456";

        public string VirtualHost { get; set; } = "";
        public string Exchange { get; set; } = "";

        public string BlogPublish_Queue { get; } = "blog.published";
        public string BlogPublish_Queue_Reply { get; } = "blog.published.outcome";

        public string UserCreated_Queue { get; } = "user.created";
        public string UserCreated_Queue_Delayed { get; } = "user.created.delayed";
    }
}