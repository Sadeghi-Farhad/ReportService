using AutoMapper;
using ReportService.Domain.Audit;
using ReportService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace ReportService.Infrastructure.Audit
{
    public class AuditCollector : IAuditCollector
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public AuditCollector(IHttpContextAccessor httpContextAccessor, IMapper mapper
            , IServiceScopeFactory ServiceScopeFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceScopeFactory = ServiceScopeFactory;
            _mapper = mapper;

        }
        public async Task<List<AuditMasterEntity>> AuditChanges(ChangeTracker changeTracker)
        {
            var now = DateTime.UtcNow;
            var result = new List<AuditMasterEntity>();

            var auditableEntries = changeTracker.Entries()
                .Where(e =>
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified ||
                     e.State == EntityState.Deleted) &&
                    e.Entity is IAuditable)
                .GroupBy(e => e.Entity.GetType().Name);

            foreach (var group in auditableEntries)
            {
                var audits = await CreateAuditAsync(group.ToList(), now);
                if (audits.Any())
                    result.AddRange(audits);
            }

            return result;
        }

        private async Task<List<AuditMasterEntity>> CreateAuditAsync(
     List<EntityEntry> entityEntries,
     DateTime timestamp)
        {
            var result = new List<AuditMasterEntity>();
            var tempChanges = new List<AuditProp>();

            bool allAddedOrDeleted =
                entityEntries.All(e => e.State == EntityState.Added) ||
                entityEntries.All(e => e.State == EntityState.Deleted);

            foreach (var entry in entityEntries)
            {
                if (entry.State == EntityState.Modified || allAddedOrDeleted)
                {
                    var change = GetChangeslist(entry);
                    if (!change.AuditPropList.Any())
                        continue;

                    result.Add(await CreateAuditMasterAsync(entry, change));
                }
                else
                {
                    tempChanges.Add(GetChangeslist(entry));
                }
            }

            foreach (var finalChange in GetFinalChangeList(tempChanges))
            {
                result.Add(await CreateAuditMasterAsync(finalChange));
            }

            return result;
        }


        private async Task<AuditMasterEntity> CreateAuditMasterAsync(
    EntityEntry entry,
    AuditProp change)
        {
            var audit = new AuditMasterEntity(
                parentId: change.Parent_Id,
                entityId: change.Entity_Id,
                entityName: entry.Entity.GetType().Name,
                action: entry.State.ToString(),
                prsCode: await GetUserNameAsync(),
                tempProperties: change.TempProperties
            )
            { Id = 0 };

            foreach (var prop in change.AuditPropList)
            {
                audit.AddDetail(new AuditDetail(
                    prop.PropertyName,
                    prop.OldValue?.ToString(),
                    prop.NewValue?.ToString()));
            }

            return audit;
        }

        private async Task<AuditMasterEntity> CreateAuditMasterAsync(AuditProp change)
        {
            var audit = new AuditMasterEntity(
                parentId: change.Parent_Id,
                entityId: change.Entity_Id,
                entityName: change.EntityName,
                action: change.State.ToString(),
                prsCode: await GetUserNameAsync(),
                tempProperties: change.TempProperties
            )
            { Id = 0 };

            foreach (var prop in change.AuditPropList)
            {
                audit.AddDetail(new AuditDetail(
                    prop.PropertyName,
                    prop.OldValue?.ToString(),
                    prop.NewValue?.ToString()));
            }

            return audit;
        }


        private AuditProp GetChangeslist(EntityEntry entry)
        {
            var result = new AuditProp
            {
                State = entry.State,
                EntityName = entry.Entity.GetType().Name
            };

            foreach (var property in entry.OriginalValues.Properties)
            {
                if (property.IsPrimaryKey())
                    continue;

                var attributes = property.PropertyInfo.CustomAttributes
                    .Select(a => a.AttributeType)
                    .ToList();

                if (attributes.Contains(typeof(ParentKeyAttribute)) ||
                    attributes.Contains(typeof(NoneAuditableAttribute)))
                    continue;

                var original = GetPropValue(entry.OriginalValues[property]);
                var current = GetPropValue(entry.CurrentValues[property]);

                if (entry.State == EntityState.Added)
                {
                    if (string.IsNullOrEmpty(current)) continue;
                    original = "-";
                }

                if (entry.State == EntityState.Deleted)
                {
                    if (string.IsNullOrEmpty(original)) continue;
                    current = "-";
                }

                if (original == current) continue;

                result.AuditPropList.Add(new AuditDetailDTO
                {
                    PropertyName = property.Name,
                    OldValue = original,
                    NewValue = current,
                    State = entry.State
                });
            }

            SetEntityKeys(entry, result);

            result.TempProperties = entry.Properties
                .Where(p => p.IsTemporary)
                .ToList();

            return result;
        }


        private static void SetEntityKeys(EntityEntry entry, AuditProp result)
        {
            var idProp = entry.Properties.Single(p => p.Metadata.IsPrimaryKey());

            var parentProp = entry.Properties.FirstOrDefault(p =>
                p.Metadata.PropertyInfo.CustomAttributes
                    .Any(a => a.AttributeType == typeof(ParentKeyAttribute)));

            if (entry.State == EntityState.Added)
            {
                result.Entity_Id = Convert.ToInt32(idProp.CurrentValue);
                result.Parent_Id = parentProp != null
                    ? Convert.ToInt32(parentProp.CurrentValue)
                    : 0;
            }
            else
            {
                result.Entity_Id = Convert.ToInt32(idProp.OriginalValue);
                result.Parent_Id = parentProp != null
                    ? Convert.ToInt32(parentProp.OriginalValue)
                    : 0;
            }
        }


        private List<AuditProp> GetFinalChangeList(List<AuditProp> changes)
        {
            var result = new List<AuditProp>();

            var deleted = changes
                .Where(c => c.State == EntityState.Deleted)
                .SelectMany(c => c.AuditPropList)
                .ToList();

            var added = changes
                .Where(c => c.State == EntityState.Added)
                .SelectMany(c => c.AuditPropList)
                .ToList();

            foreach (var item in changes)
            {
                foreach (var change in item.AuditPropList)
                {
                    if (change.State == EntityState.Added &&
                        !deleted.Any(d => d.OldValue == change.NewValue))
                    {
                        result.Add(item);
                        break;
                    }

                    if (change.State == EntityState.Deleted &&
                        !added.Any(a => a.NewValue == change.OldValue))
                    {
                        result.Add(item);
                        break;
                    }
                }
            }

            return result;
        }


        private static string GetPropValue(object value)
        {
            return value?.ToString()?.Trim() ?? string.Empty;
        }

        private async Task<int> GetUserNameAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var uid = user.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
                return Convert.ToInt32(uid);
            }

            throw new BaseException(
                "Audit error in GetUserNameAsync: attempt to audit an unauthorized user.");

        }
    }

    internal class AuditDetailDTO
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public EntityState? State { get; set; }
    }

    internal class AuditProp
    {
        public int Parent_Id { get; set; } = 0;
        public int Entity_Id { get; set; }
        public EntityState? State { get; set; }
        public string? EntityName { get; set; }
        public List<AuditDetailDTO> AuditPropList { get; set; } = new List<AuditDetailDTO>();
        public List<PropertyEntry> TempProperties { get; set; }
    }
}
