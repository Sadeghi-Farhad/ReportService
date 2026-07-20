using ReportService.Application.Audit.Common;
using ReportService.Application.Audit.Projections;
using ReportService.Domain.Blogs;

namespace ReportService.Application.Audit.Queries.GetByParentId
{
    class GetByParentIdHandler(IAuditRepository repository,IAuditProjection auditProjection)
        : IRequestHandler<GetByParentIdQuery, List<AuditResult>>
    {

        public async Task<List<AuditResult>> Handle(GetByParentIdQuery request, CancellationToken cancellationToken)
        {
            var Audits = await repository.GetAuditResultWithParentId(request.ParentId, cancellationToken);
            var Result=await auditProjection.ProjectAsync(Audits);
            return Result;
        }      
    }
}
