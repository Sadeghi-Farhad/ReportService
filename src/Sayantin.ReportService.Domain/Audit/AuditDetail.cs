using ReportService.Domain.Base;

namespace ReportService.Domain.Audit
{
    public class AuditDetail
    {
        public int Id { get; set; }
        public int AuditMasterId { get; private set; }
        public string PropertyName { get; private set; }
        public string OldValue { get; private set; }
        public string NewValue { get; private set; }

        private AuditMaster _auditMaster;
        public AuditMaster AuditMaster => _auditMaster;

        private AuditDetail() { }

        public AuditDetail( string propertyName, string oldValue, string newValue)
        {           
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }


        public void SetPropertyName(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

}
