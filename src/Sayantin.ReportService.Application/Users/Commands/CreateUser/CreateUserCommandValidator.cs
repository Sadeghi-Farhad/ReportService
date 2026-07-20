namespace ReportService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام الزامی است.");
            RuleFor(x => x.Birthday).NotEmpty().WithMessage("تاریخ تولد الزامی است.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("ایمیل الزامی است.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("ایمیل نامعتبر است");
        }
    }
}