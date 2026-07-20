using ReportService.Domain.Interfaces;

namespace ReportService.Domain.Blogs
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        Task<List<Blog>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default);
    }
}