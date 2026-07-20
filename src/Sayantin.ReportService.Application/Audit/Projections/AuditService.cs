using ReportService.Domain.Audit;
using ReportService.Domain.Users;

namespace ReportService.Application.Audit.Projections
{
    class AuditService(IUserRepository userRepository) : IAuditService
    {
        public async Task<string> GetValueForAudit(Type typeName, string inputId)
        {
            if (!int.TryParse(inputId, out int id))
                return "-";
            
            if (typeName == typeof(IUserRepository))
            {
                var entity = await userRepository.GetByIdAsync(id);
                return entity?.Name ?? "-"; 
            }

            // TODO: handle other repositories
            // else if (type == typeof(IOtherRepository)) { ... }

            return "-";
        }
    }
}
