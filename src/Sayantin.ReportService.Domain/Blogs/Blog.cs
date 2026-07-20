using System.ComponentModel;
using ReportService.Domain.Audit;
using ReportService.Domain.Base;
using ReportService.Domain.Users;

namespace ReportService.Domain.Blogs
{
    /// <summary>
    /// Entity class for Blog Aggregate.
    /// </summary>
    [DisplayName("پست")]
    public partial class Blog : BaseEntity<int> , IAuditable
    {
        [DisplayName("عنوان")]
        public string Title { get; private set; }

        [DisplayName("توضیحات")]
        public string Description { get; private set; }

        [DisplayName("نویسنده")]
        [GetValueFrom(typeof(IUserRepository))]
        public int AuthorId { get; private set; }

        [DisplayName("وضعیت انتشار")]
        public bool IsPublished { get; private set; } = false;

        [DisplayName("وضعیت تحویل")]
        public bool IsDelivered { get; private set; } = false;

        public virtual User Author { get; set; }

        public Blog()
        {
        }
    }
}