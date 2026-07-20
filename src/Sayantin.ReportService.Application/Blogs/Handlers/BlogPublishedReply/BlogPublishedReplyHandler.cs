using ReportService.Domain.Blogs;
using ReportService.Domain.Interfaces;

namespace ReportService.Application.Blogs.Handlers.BlogPublishedReply
{
    public class BlogPublishedReplyHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<BlogPublishedReply, bool>
    {
        public async Task<bool> Handle(BlogPublishedReply request, CancellationToken ct)
        {
            var blog = await blogRepository.GetByIdAsync(request.Id);
            if (blog == null) throw new Domain.Exceptions.KeyNotFoundException($"بلاگ با شناسه {request.Id} یافت نشد");

            blog.Deliver();

            await unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}
