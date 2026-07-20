namespace ReportService.Domain.Audit
{
    public interface IAuditService
    {
        public Task<string> GetValueForAudit(Type typeName, string inputId);
    }
}
