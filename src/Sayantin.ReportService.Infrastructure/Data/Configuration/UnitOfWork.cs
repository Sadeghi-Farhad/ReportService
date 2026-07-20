using ReportService.Domain.Base;
using ReportService.Domain.Interfaces;
using ReportService.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Data.Configuration
{
    public class UnitOfWork(EFDbContext dbContext, IPublisher? publisher) : IUnitOfWork
    {
        public IBaseRepository<T> Repository<T>()
            where T : BaseEntity
        {
            return new BaseRepository<T>(dbContext);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            int result = await dbContext.SaveChangesAsync(cancellationToken);

            // ignore events if no publisher provided
            if (publisher == null) return result;

            // dispatch events only if save was successful
            await DispatchDomainEvents(dbContext, publisher);

            return result;
        }

        private async Task DispatchDomainEvents(DbContext? context, IPublisher publisher)
        {
            if (context == null) return;

            var entitiesWithEvents = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.Events.Any())
                .Select(e => e.Entity);

            var domainEvents = entitiesWithEvents
                .SelectMany(e => e.Events)
                .ToList();

            entitiesWithEvents.ToList().ForEach(e => e.ClearEvents());

            foreach (var @event in domainEvents)
                await publisher.Publish(@event);
        }
    }
}