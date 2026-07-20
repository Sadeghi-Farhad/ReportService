using ReportService.Domain.Base;

namespace ReportService.Domain.Blogs.Events
{
    public sealed class BlogPublishedEvent : BaseDomainEvent
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = string.Empty;
    }
}