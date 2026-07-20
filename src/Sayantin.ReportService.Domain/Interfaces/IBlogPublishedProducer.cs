using ReportService.Domain.Blogs.Events;

namespace ReportService.Domain.Interfaces
{
    public interface IBlogPublishedProducer
    {
        Task PublishAsync(BlogPublishedEvent blog, CancellationToken cancellationToken = default);
    }
}
