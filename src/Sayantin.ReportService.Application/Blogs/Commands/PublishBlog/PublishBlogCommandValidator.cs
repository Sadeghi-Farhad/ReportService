namespace ReportService.Application.Blogs.Commands.PublishBlog
{
    public class PublishBlogCommandValidator : AbstractValidator<PublishBlogCommand>
    {
        public PublishBlogCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است.");
        }
    }
}
