using ReportService.Domain.Blogs;
using ReportService.Domain.Interfaces;

namespace ReportService.Application.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommandHandler(IBlogRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteBlogCommand, bool>
    {
        public async Task<bool> Handle(DeleteBlogCommand request, CancellationToken ct)
        {
            var blog = await repository.GetByIdAsync(request.Id);

            if (blog == null || blog.Id == 0)
                throw new Domain.Exceptions.KeyNotFoundException($"بلاگ با شناسه {request.Id} یافت نشد");

            var result = await repository.DeleteAsync(blog);
            await unitOfWork.SaveChangesAsync(ct);

            return result;
        }
    }
}
