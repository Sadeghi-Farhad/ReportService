namespace ReportService.Application.Audit.Common
{
    public class AuditDetailResult
    {
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
