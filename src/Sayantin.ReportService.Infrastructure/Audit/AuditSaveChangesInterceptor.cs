using ReportService.Domain.Audit;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ReportService.Infrastructure.Audit
{
    public sealed class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private List<AuditMasterEntity>? _auditEntries;
        private readonly IAuditCollector _auditService;

        public AuditSaveChangesInterceptor(IAuditCollector auditService)
        {
            _auditService = auditService;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return result;

            _auditEntries = await _auditService.AuditChanges(context.ChangeTracker);

            return result;
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null || _auditEntries == null || _auditEntries.Count == 0)
                return result;

            foreach (var entry in _auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.SetEntityId(Convert.ToInt32(prop.CurrentValue));
                    }
                    else if (prop.Metadata.PropertyInfo?
                        .GetCustomAttributes(typeof(ParentKeyAttribute), false)
                        .Any() == true)
                    {
                        entry.SetParentId(Convert.ToInt32(prop.CurrentValue));
                    }
                }
            }

            // Prevent recursion
            if (!context.ChangeTracker.Entries<AuditMaster>().Any())
            {
                context.Set<AuditMaster>().AddRange(_auditEntries);
                await context.SaveChangesAsync(cancellationToken);
            }

            _auditEntries = null;
            return result;
        }
    }
}
