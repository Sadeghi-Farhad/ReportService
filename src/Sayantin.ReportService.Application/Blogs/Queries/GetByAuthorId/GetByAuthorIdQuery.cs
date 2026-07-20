using ReportService.Application.Blogs.Common;

namespace ReportService.Application.Blogs.Queries.GetByAuthorId
{
    public class GetByAuthorIdQuery : IRequest<List<BlogResult>>
    {
        public int AuthorId { get; set; }
    }
}
