namespace ReportService.Application.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
