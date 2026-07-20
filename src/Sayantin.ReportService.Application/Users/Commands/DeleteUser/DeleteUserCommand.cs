namespace ReportService.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}