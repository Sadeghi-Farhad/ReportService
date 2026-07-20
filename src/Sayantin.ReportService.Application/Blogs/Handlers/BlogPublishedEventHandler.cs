using ReportService.Domain.Blogs.Events;
using ReportService.Domain.Interfaces;

namespace ReportService.Application.Blogs.Handlers
{
    internal class BlogPublishedEventHandler(IEmailService emailSender, IBlogPublishedProducer messageBrokerService) : INotificationHandler<BlogPublishedEvent>
    {
        public async Task Handle(BlogPublishedEvent domainEvent, CancellationToken cancellationToken)
        {
            await emailSender.SendEmailAsync("m.mostafamoradi@crouse.ir", //f.sadeghi@crouse.ir
                                             "crouseservicetemplate@crouse.ir",
                                             "ReportService",
                                             $"Blog with id {domainEvent.BlogId} was published.");

            await messageBrokerService.PublishAsync(domainEvent, cancellationToken);
        }
    }
}