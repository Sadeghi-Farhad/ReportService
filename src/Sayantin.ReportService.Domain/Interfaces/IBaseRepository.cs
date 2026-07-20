using System.Linq.Expressions;
using ReportService.Domain.Base;

namespace ReportService.Domain.Interfaces
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
            where TKey : notnull;

        Task<List<T>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<T>> GetListByIdAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);


        // ToDo
        // Task<T> GetAsync(Expression<Func<T, bool>> expression);

        // ToDo
        //Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    }
}