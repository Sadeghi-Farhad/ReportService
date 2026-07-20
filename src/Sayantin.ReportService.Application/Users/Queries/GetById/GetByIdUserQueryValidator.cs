namespace ReportService.Application.Users.Queries.GetById
{
    public class GetByIdUserQueryValidator : AbstractValidator<GetByIdUserQuery>
    {
        public GetByIdUserQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه نامعتبر است.");
        }
    }
}