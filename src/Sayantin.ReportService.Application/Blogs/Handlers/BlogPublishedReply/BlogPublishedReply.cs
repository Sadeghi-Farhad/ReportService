namespace ReportService.Application.Blogs.Handlers.BlogPublishedReply
{
    public class BlogPublishedReply : IRequest<bool>
    {
        public int Id { get; set; }
        public string Result { get; set; } = string.Empty;
    }
}
