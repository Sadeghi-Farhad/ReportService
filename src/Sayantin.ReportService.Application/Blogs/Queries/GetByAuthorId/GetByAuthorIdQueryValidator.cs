namespace ReportService.Application.Blogs.Queries.GetByAuthorId
{
    public class GetByAuthorIdQueryValidator : AbstractValidator<GetByAuthorIdQuery>
    {
        public GetByAuthorIdQueryValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("شناسه نویسنده نامعتبر است.");
        }
    }
}
