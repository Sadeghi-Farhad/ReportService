using ReportService.Domain.Audit;
using ReportService.Domain.Interfaces;

namespace ReportService.Domain.Blogs
{
    public interface IAuditRepository : IBaseRepository<AuditMaster>
    {
        Task<List<AuditMaster>> GetAuditResultWithParentId(int parentId, CancellationToken cancellationToken = default);
    }
}