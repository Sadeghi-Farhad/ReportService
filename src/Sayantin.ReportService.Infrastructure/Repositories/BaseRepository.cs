using ReportService.Domain.Base;
using ReportService.Domain.Interfaces;
using ReportService.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ReportService.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(EFDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public virtual Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public virtual Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        public virtual async Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull
        {
            return await _dbSet.FindAsync(new object[1] { id }, cancellationToken);
        }

        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }


        public async Task<List<T>> GetListByIdAsync(
    Expression<Func<T, bool>> predicate,
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }






        // ToDo
        //public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        //{
        //    return _dbSet.FirstOrDefaultAsync(expression);
        //}

        // ToDo
        //public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        //{
        //    return _dbSet.Where(expression).ToListAsync();
        //}
    }
}