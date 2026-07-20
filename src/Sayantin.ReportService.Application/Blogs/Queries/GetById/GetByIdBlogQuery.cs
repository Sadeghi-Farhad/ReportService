using ReportService.Application.Blogs.Common;

namespace ReportService.Application.Blogs.Queries.GetById
{
    public class GetByIdBlogQuery : IRequest<BlogResult>
    {
        public int Id { get; set; }
    }
}
