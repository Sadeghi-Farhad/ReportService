namespace ReportService.Application.Audit.Common
{
    public class AuditResult
    {
        public string? EntityName { get; set; }
        public string Fullname { get; set; }
        public string TimeStamp { get; set; }
        public string Action { get; set; }
        public List<AuditDetailResult> Changes { get; set; } = new List<AuditDetailResult>();
    }
}
