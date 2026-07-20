
namespace ReportService.Domain.Audit
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParentKeyAttribute : Attribute { }


    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NoneAuditableAttribute : Attribute { }


    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotShowAuditAttribute : Attribute { }

    public class GetValueFromAttribute : Attribute
    {
        public Type _type { get; set; }
        public GetValueFromAttribute(Type type)
        {
            _type = type;
        }

    }
}
