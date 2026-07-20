using ReportService.Application.Audit.Common;
using ReportService.Domain.Audit;
using System.ComponentModel;
using System.Reflection;
using ReportService.Application.Audit.Projections.Formatting;
using ReportService.Domain.Common;
using ReportService.Domain.Personnels;

namespace ReportService.Application.Audit.Projections
{
    public class AuditProjection(IAuditValueFormatter auditFormatter, IPersonnelInfo personnelInfo, IAuditService auditService) : IAuditProjection
    {
        //public async Task<List<AuditResult>> ProjectAsync(List<AuditMaster> Audits)
        //{
        //    List<AuditResult> Result = new();

        //    foreach (var item in Audits)
        //    {
        //        //get Editor Name
        //        var AuditTmp = new AuditResult();
        //        var AuditTmp2 = new List<AuditDetailResult>();


        //        var personnels = await personnelInfo.GetPersonnels(new PersonnelSearchInput(new List<int> { item.PrsCode }));
        //        AuditTmp.Fullname = (personnels != null && personnels.Count > 0) ? personnels.FirstOrDefault().FirstName + " " + personnels.FirstOrDefault().LastName : "";



        //        //var EntityType = Type.GetType("CrouseServiceNewsPanel.Models." + item.EntityName);

        //        var EntityType = AppDomain.CurrentDomain
        //                        .GetAssemblies()
        //                        .SelectMany(a => a.GetTypes())
        //                        .FirstOrDefault(t => t.Name == item.EntityName);
        //        if (EntityType == null) continue;

        //        var displayName = EntityType.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
        //        AuditTmp.EntityName = displayName != null ? displayName.DisplayName : "";

        //        AuditTmp.Action = auditFormatter.FormatAction(item.Action);

        //        //Get DateTime
        //        AuditTmp.TimeStamp = DateHelper.ConvertToShamsiDateTime(item.TimeStamp);

        //        foreach (var prop in item.AuditDetails)
        //        {
        //            var propTmp = new AuditDetailResult();

        //            if (prop.PropertyName.Contains('('))
        //            {
        //                prop.SetPropertyName(prop.PropertyName.Remove(prop.PropertyName.Length - 7, 7));
        //            }

        //            var pinfo = EntityType.GetProperty(prop.PropertyName);
        //            if (pinfo == null) continue;

        //            var Attributes = pinfo.GetCustomAttributes();
        //            //dont get Audit
        //            if (Attributes.Any(p => p.GetType() == typeof(NotShowAuditAttribute) || p.GetType() == typeof(NoneAuditableAttribute))) continue;

        //            displayName = Attributes.Where(p => p.GetType() == typeof(DisplayNameAttribute)).FirstOrDefault() as DisplayNameAttribute;
        //            propTmp.FieldName = displayName != null ? displayName.DisplayName : prop.PropertyName;


        //            //get string value from foreign key
        //            if (Attributes.Any(p => p.GetType() == typeof(GetValueFromAttribute)))
        //            {
        //                var TypeValue = (Attributes.Where(p => p.GetType() == typeof(GetValueFromAttribute)).FirstOrDefault() as GetValueFromAttribute)?._functionName;
        //                if (TypeValue != null)
        //                {
        //                    propTmp.NewValue = await auditService.GetValueForAudit(typeof(Type), prop.NewValue);
        //                    propTmp.OldValue = await auditService.GetValueForAudit(typeof(Type), prop.OldValue);
        //                }
        //                else
        //                    continue;
        //            }
        //            else
        //            {
        //                if (pinfo.PropertyType == typeof(DateTime?))
        //                {
        //                    propTmp.NewValue = DateTime.TryParse(prop.NewValue, out DateTime dateResult) ? DateHelper.ConvertToShamsiDateTime(dateResult) : prop.NewValue;
        //                    propTmp.OldValue = DateTime.TryParse(prop.OldValue, out DateTime dateResult2) ? DateHelper.ConvertToShamsiDateTime(dateResult) : prop.OldValue; ;
        //                }
        //                else if (pinfo.PropertyType == typeof(Boolean))
        //                {
        //                    propTmp.NewValue = auditFormatter.FormatBoolean(prop.NewValue);
        //                    propTmp.OldValue = auditFormatter.FormatBoolean(prop.OldValue);
        //                }
        //                else
        //                {
        //                    propTmp.NewValue = prop.NewValue;
        //                    propTmp.OldValue = prop.OldValue;
        //                }
        //            }

        //            AuditTmp2.Add(propTmp);
        //        }

        //        AuditTmp.Changes.AddRange(AuditTmp2);

        //        Result.Add(AuditTmp);

        //    }
        //    return Result;
        //}


        public async Task<List<AuditResult>> ProjectAsync(List<AuditMaster> audits)
        {
            var result = new List<AuditResult>();

            foreach (var audit in audits)
            {
                var auditResult = new AuditResult();

                // --- Set Editor Name ---
                var personnels = await personnelInfo.GetPersonnels(
                    new PersonnelSearchInput(new List<int> { audit.PrsCode })
                );
                var person = personnels?.FirstOrDefault();
                auditResult.Fullname = person != null
                    ? $"{person.FirstName} {person.LastName}"
                    : string.Empty;

                // --- Get Entity Type ---
                var entityType = GetEntityTypeByName(audit.EntityName);
                if (entityType == null) continue;

                auditResult.EntityName = GetDisplayName(entityType);
                auditResult.Action = auditFormatter.FormatAction(audit.Action);
                auditResult.TimeStamp = DateHelper.ConvertToShamsiDateTime(audit.TimeStamp);

                // --- Map Audit Details ---
                var details = await MapAuditDetailsAsync(audit.AuditDetails, entityType);
                auditResult.Changes.AddRange(details);

                result.Add(auditResult);
            }

            return result;
        }

        // --- Helper: Get entity Type by string ---
        private Type? GetEntityTypeByName(string entityName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == entityName);
        }

        // --- Helper: Get DisplayName attribute or fallback ---
        private string GetDisplayName(Type entityType)
        {
            var attr = entityType.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                                 .FirstOrDefault() as DisplayNameAttribute;
            return attr?.DisplayName ?? entityType.Name;
        }

        // --- Helper: Map audit details ---
        private async Task<List<AuditDetailResult>> MapAuditDetailsAsync(
            IReadOnlyCollection<AuditDetail> details,
            Type entityType)
        {
            var result = new List<AuditDetailResult>();

            foreach (var detail in details)
            {
                var propResult = new AuditDetailResult();
                var propName = CleanPropertyName(detail.PropertyName);
                var propInfo = entityType.GetProperty(propName);
                if (propInfo == null) continue;

                var attributes = propInfo.GetCustomAttributes();
                if (attributes.Any(a => a.GetType() == typeof(NotShowAuditAttribute) || a.GetType() == typeof(NoneAuditableAttribute)))
                    continue;

                propResult.FieldName = GetPropertyDisplayName(propInfo);

                if (attributes.Any(a => a.GetType() == typeof(GetValueFromAttribute)))
                {
                    var type = (attributes.First(a => a.GetType() == typeof(GetValueFromAttribute)) as GetValueFromAttribute)?._type;
                    if (type!=null)
                    {
                        propResult.NewValue = await auditService.GetValueForAudit(type, detail.NewValue);
                        propResult.OldValue = await auditService.GetValueForAudit(type, detail.OldValue);
                    }
                }
                else
                {
                    MapPropertyValue(propInfo, detail, propResult);
                }

                result.Add(propResult);
            }

            return result;
        }

        // --- Helper: Clean property name ---
        private string CleanPropertyName(string propertyName)
        {
            return propertyName.Contains('(')
                ? propertyName.Remove(propertyName.Length - 7, 7)
                : propertyName;
        }

        // --- Helper: Get property DisplayName ---
        private string GetPropertyDisplayName(PropertyInfo propInfo)
        {
            var attr = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                               .FirstOrDefault() as DisplayNameAttribute;
            return attr?.DisplayName ?? propInfo.Name;
        }

        // --- Helper: Map value types ---
        private void MapPropertyValue(PropertyInfo propInfo, AuditDetail detail, AuditDetailResult propResult)
        {
            switch (Type.GetTypeCode(propInfo.PropertyType))
            {
                case TypeCode.Boolean:
                    propResult.NewValue = auditFormatter.FormatBoolean(detail.NewValue);
                    propResult.OldValue = auditFormatter.FormatBoolean(detail.OldValue);
                    break;

                case TypeCode.DateTime:
                    propResult.NewValue = DateTime.TryParse(detail.NewValue, out var newDate)
                        ? DateHelper.ConvertToShamsiDateTime(newDate)
                        : detail.NewValue;

                    propResult.OldValue = DateTime.TryParse(detail.OldValue, out var oldDate)
                        ? DateHelper.ConvertToShamsiDateTime(oldDate)
                        : detail.OldValue;
                    break;

                default:
                    propResult.NewValue = detail.NewValue;
                    propResult.OldValue = detail.OldValue;
                    break;
            }
        }

    }
}
