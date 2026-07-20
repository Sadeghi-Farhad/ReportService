namespace ReportService.Application.Audit.Projections.Formatting
{
    public class AuditValueFormatter : IAuditValueFormatter
    {
        public string FormatAction(string action) =>
        action switch
        {
            "Added" => "اضافه شده",
            "Deleted" => "پاک شده",
            "Modified" => "ویرایش شده",
            _ => action
        };

        public string FormatBoolean(string input) =>
            input?.ToLower() switch
            {
                "true" => "بله",
                "false" => "خیر",
                _ => input
            };
    }
}
