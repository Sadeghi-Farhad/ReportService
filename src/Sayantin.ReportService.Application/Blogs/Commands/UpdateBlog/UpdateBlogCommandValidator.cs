namespace ReportService.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه بلاگ نامعتبر است.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("موضوع الزامی است.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات الزامی است.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("توضیحات نباید بیشتر از 500 کاراکتر باشد.");
            RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("شناسه نویسنده نامعتبر است.");
        }
    }
}
