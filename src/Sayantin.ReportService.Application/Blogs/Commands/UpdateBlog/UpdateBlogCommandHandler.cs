using ReportService.Application.Blogs.Common;
using ReportService.Domain.Blogs;
using ReportService.Domain.Interfaces;

namespace ReportService.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler(IBlogRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateBlogCommand, BlogResult>
    {
        public async Task<BlogResult> Handle(UpdateBlogCommand request, CancellationToken ct)
        {
            var blog = await repository.GetByIdAsync(request.Id);

            if (blog == null || blog.Id == 0)
                throw new Domain.Exceptions.KeyNotFoundException($"بلاگ با شناسه {request.Id} یافت نشد");

            var input = mapper.Map<Blog>(request);
            var result = await repository.UpdateAsync(input);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<BlogResult>(result);
        }
    }
}
