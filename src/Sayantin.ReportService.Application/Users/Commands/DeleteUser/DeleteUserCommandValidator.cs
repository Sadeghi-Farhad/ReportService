using FluentValidation;

namespace ReportService.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("شناسه نامعتبر است.");
        }
    }
}