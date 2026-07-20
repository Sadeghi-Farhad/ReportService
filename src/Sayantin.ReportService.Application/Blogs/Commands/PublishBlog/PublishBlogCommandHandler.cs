using ReportService.Domain.Blogs;
using ReportService.Domain.Interfaces;

namespace ReportService.Application.Blogs.Commands.PublishBlog
{
    public class PublishBlogCommandHandler(IBlogRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<PublishBlogCommand, bool>
    {
        public async Task<bool> Handle(PublishBlogCommand request, CancellationToken ct)
        {
            var blog = await repository.GetByIdAsync(request.Id);

            if (blog == null || blog.Id == 0)
                throw new Domain.Exceptions.KeyNotFoundException($"بلاگ با شناسه {request.Id} یافت نشد");

            blog.Publish();
            await unitOfWork.SaveChangesAsync(ct);

            return true;
        }
    }
}
