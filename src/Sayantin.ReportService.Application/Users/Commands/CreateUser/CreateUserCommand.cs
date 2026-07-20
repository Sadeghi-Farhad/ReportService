using ReportService.Application.Users.Common;

namespace ReportService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserResult>
    {
        public string Name { get; set; }
        public DateOnly Birthday { get; set; }
        public string Email { get; set; }
    }
}
