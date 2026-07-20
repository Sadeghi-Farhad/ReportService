namespace ReportService.Application.Blogs.Queries.GetById
{
    public class GetByIdBlogQueryValidator : AbstractValidator<GetByIdBlogQuery>
    {
        public GetByIdBlogQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است.");
        }
    }
}
