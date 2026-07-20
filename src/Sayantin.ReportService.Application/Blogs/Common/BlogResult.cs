namespace ReportService.Application.Blogs.Common
{
    public class BlogResult
    {
        /// <summary>
        /// شناسه بلاگ
        /// </summary>
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}