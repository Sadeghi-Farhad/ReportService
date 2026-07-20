using ReportService.Application.Blogs.Common;

namespace ReportService.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<BlogResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}