using ReportService.Domain.Base;
using ReportService.Domain.Blogs;
using ReportService.Domain.Common;

namespace ReportService.Domain.Users
{
    /// <summary>
    /// Entity class for User Aggregate.
    /// </summary>
    public partial class User : BaseEntity<int>
    {
        public string Name { get; private set; }
        public DateOnly Birthday { get; private set; }
        public int Age => Functions.CalculateAge(Birthday);
        public string Email { get; private set; }
        public AddressValueObject? Address { get; private set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public User()
        {
            Blogs = new HashSet<Blog>();
        }
    }
}