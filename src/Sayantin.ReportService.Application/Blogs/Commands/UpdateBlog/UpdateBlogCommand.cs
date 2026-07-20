using ReportService.Application.Blogs.Common;

namespace ReportService.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommand : IRequest<BlogResult>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
