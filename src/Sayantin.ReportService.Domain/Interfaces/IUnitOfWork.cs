using ReportService.Domain.Base;

namespace ReportService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IBaseRepository<T> Repository<T>()
            where T : BaseEntity;
    }
}