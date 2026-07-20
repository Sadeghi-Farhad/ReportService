namespace ReportService.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان الزامی است.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات الزامی است.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("توضیحات نباید بیشتر از 500 کاراکتر باشد.");
            RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("شناسه نویسنده نامعتبر است.");
        }
    }
}