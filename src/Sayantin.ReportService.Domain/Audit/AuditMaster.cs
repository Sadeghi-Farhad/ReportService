using ReportService.Domain.Base;

namespace ReportService.Domain.Audit
{
    public class AuditMaster: BaseEntity<int>
    {
        private readonly List<AuditDetail> _auditDetails = new();
        public int ParentId { get; private set; }
        public int EntityId { get; private set; }
        public string EntityName { get; private set; }
        public string Action { get; private set; }
        public int PrsCode { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public IReadOnlyCollection<AuditDetail> AuditDetails => _auditDetails;
        public AuditMaster() { }

        public AuditMaster(int parentId, int entityId, string entityName, string action, int prsCode)
        {
            ParentId = parentId;
            EntityId = entityId;
            EntityName = entityName;
            Action = action;
            PrsCode = prsCode;
            TimeStamp = DateTime.UtcNow;
        }

        public void SetEntityId(int entityId)
        {
            EntityId = entityId;
        }

        public void SetParentId(int parentId)
        {
            ParentId = parentId;
        }

        public void AddDetail(AuditDetail detail)
        {
            _auditDetails.Add(detail);
        }
    }
}
