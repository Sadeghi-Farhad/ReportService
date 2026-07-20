using ReportService.Application.Blogs.Common;
using ReportService.Domain.Blogs;

namespace ReportService.Application.Blogs.Queries.GetAll
{
    public class GetAllBlogsQueryHandler(IBlogRepository repository, IMapper mapper) : IRequestHandler<GetAllBlogsQuery, List<BlogResult>>
    {
        public async Task<List<BlogResult>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
        {
            var blogs = await repository.ListAsync(cancellationToken);
            return mapper.Map<List<BlogResult>>(blogs);
        }
    }
}