using ReportService.Domain.Blogs;
using ReportService.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Repositories
{
    public class BlogRepository(EFDbContext context) : BaseRepository<Blog>(context), IBlogRepository
    {
        public override async Task<Blog?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            return await base.GetByIdAsync<TKey>(id, cancellationToken);
        }

        public override async Task<List<Blog>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(m => m.Author).ToListAsync();
        }

        public async Task<List<Blog>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(m => m.Author).Where(m => m.AuthorId == authorId).ToListAsync(cancellationToken);
        }
    }
}
