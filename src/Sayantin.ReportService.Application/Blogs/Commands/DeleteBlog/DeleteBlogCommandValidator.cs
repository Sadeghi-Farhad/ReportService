namespace ReportService.Application.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommandValidator : AbstractValidator<DeleteBlogCommand>
    {
        public DeleteBlogCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است.");
        }
    }
}
