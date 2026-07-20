using ReportService.Domain.Audit;
using ReportService.Domain.Blogs;
using ReportService.Infrastructure.Data.Configuration;

namespace ReportService.Infrastructure.Repositories
{
    public class AuditRepository(EFDbContext context) : BaseRepository<AuditMaster>(context), IAuditRepository
    {
        public async Task<List<AuditMaster>> GetAuditResultWithParentId(int parentId, CancellationToken cancellationToken = default)
        {
            var Audits = await base.GetListByIdAsync(a => a.ParentId == parentId || a.EntityId == parentId, a => a.AuditDetails);
            var EntityIds = Audits.Where(e => e.EntityId != parentId).Select(e => e.EntityId).ToList();
            if (EntityIds.Count > 0)
            {
                var tmpAudit = await base.GetListByIdAsync(e => e.ParentId != 0 && EntityIds.Contains(e.ParentId), a => a.AuditDetails);
                Audits.AddRange(tmpAudit);
            }
            Audits = Audits.OrderBy(e => e.TimeStamp).ToList();

            return Audits;
        }
    }
}
