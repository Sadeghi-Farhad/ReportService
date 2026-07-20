namespace ReportService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserAddressCommandValidator : AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("شناسه کاربر نامعتبر است.");
            RuleFor(x => x.Province).NotEmpty().WithMessage("نام استان الزامی است.");
            RuleFor(x => x.City).NotEmpty().WithMessage("نام شهر الزامی است.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("نام خیابان الزامی است.");
        }
    }
}