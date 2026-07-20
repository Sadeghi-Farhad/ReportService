using FluentValidation;

namespace ReportService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه کاربر نامعتبر است");
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام الزامی است.");
            RuleFor(x => x.Birthday).NotEmpty().WithMessage("تاریخ تولد الزامی است.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("ایمیل الزامی است.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("ایمیل نامعتبر است");
        }
    }
}