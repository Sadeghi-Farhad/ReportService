namespace ReportService.Application.Blogs.Commands.PublishBlog
{
    public class PublishBlogCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
