using ReportService.Application.Blogs.Common;
using ReportService.Domain.Blogs;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Users;

namespace ReportService.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandHandler(IBlogRepository blogRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateBlogCommand, BlogResult>
    {
        public async Task<BlogResult> Handle(CreateBlogCommand request, CancellationToken ct)
        {
            var user = await userRepository.GetByIdAsync(request.AuthorId);
            if (user == null) throw new Domain.Exceptions.KeyNotFoundException($"نویسنده با شناسه {request.AuthorId} یافت نشد");

            var blog = mapper.Map<Blog>(request);
            var createdBlog = await blogRepository.AddAsync(blog, ct);

            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<BlogResult>(createdBlog);
        }
    }
}