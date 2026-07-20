using ReportService.Application.Blogs.Common;
using ReportService.Domain.Blogs;

namespace ReportService.Application.Blogs.Queries.GetById
{
    public class GetByIdBlogQueryHandler(IBlogRepository repository, IMapper mapper)
        : IRequestHandler<GetByIdBlogQuery, BlogResult>
    {
        public async Task<BlogResult> Handle(GetByIdBlogQuery request, CancellationToken ct)
        {
            var blog = await repository.GetByIdAsync(request.Id);

            if (blog == null || blog.Id == 0)
                throw new Domain.Exceptions.KeyNotFoundException($"بلاگ با شناسه {request.Id} یافت نشد"); ;

            return mapper.Map<BlogResult>(blog);
        }
    }
}