using ReportService.Application.Users.Common;

namespace ReportService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Birthday { get; set; }
        public string Email { get; set; }
    }
}