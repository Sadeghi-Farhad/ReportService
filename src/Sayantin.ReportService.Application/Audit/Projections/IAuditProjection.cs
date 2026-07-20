using ReportService.Application.Audit.Common;
using ReportService.Domain.Audit;

namespace ReportService.Application.Audit.Projections
{
    public interface IAuditProjection
    {
        Task<List<AuditResult>> ProjectAsync(List<AuditMaster> audits);
    }
}
