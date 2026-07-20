using ReportService.Application.Blogs.Common;
using ReportService.Domain.Blogs;

namespace ReportService.Application.Blogs.Queries.GetByAuthorId
{
    public class GetByAuthorIdQueryHandler(IBlogRepository repository, IMapper mapper)
        : IRequestHandler<GetByAuthorIdQuery, List<BlogResult>>
    {
        public async Task<List<BlogResult>> Handle(GetByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            var blogs = await repository.GetByAuthorIdAsync(request.AuthorId, cancellationToken);
            return mapper.Map<List<BlogResult>>(blogs);
        }
    }
}