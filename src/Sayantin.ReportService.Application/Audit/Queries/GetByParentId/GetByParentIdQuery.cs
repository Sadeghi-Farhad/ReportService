using ReportService.Application.Audit.Common;

namespace ReportService.Application.Audit.Queries.GetByParentId
{
    public class GetByParentIdQuery : IRequest<List<AuditResult>>
    {
        public int ParentId { get; set; }
    }
}
