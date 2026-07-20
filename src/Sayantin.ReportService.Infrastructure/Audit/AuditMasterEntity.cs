using ReportService.Domain.Audit;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace ReportService.Infrastructure.Audit
{
    public class AuditMasterEntity : AuditMaster
    {
        public List<PropertyEntry> TempProperties { get; set; }

        public AuditMasterEntity(
        int parentId,
        int entityId,
        string entityName,
        string action,
        int prsCode,
         List<PropertyEntry> tempProperties)
        : base(parentId, entityId, entityName, action, prsCode)
        {
            TempProperties = tempProperties;
        }
    }
}
