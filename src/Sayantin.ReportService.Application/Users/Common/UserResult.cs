using ReportService.Domain.Users;

namespace ReportService.Application.Users.Common
{
    public class UserResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Birthday { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    public class UserResultWithAddress : UserResult
    {
        public AddressValueObject Address { get; set; }
    }
}