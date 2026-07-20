using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReportService.Infrastructure.Audit
{
    public interface IAuditCollector
    {
        Task<List<AuditMasterEntity>> AuditChanges(ChangeTracker changeTracker);
    }
}
