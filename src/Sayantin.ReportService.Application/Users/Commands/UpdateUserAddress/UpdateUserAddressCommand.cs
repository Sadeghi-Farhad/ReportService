using ReportService.Application.Users.Common;

namespace ReportService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserAddressCommand : IRequest<UserResultWithAddress>
    {
        public int UserId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? PostalCode { get; set; }
    }
}