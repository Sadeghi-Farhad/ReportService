using ReportService.Application.Users.Common;

namespace ReportService.Application.Users.Queries.GetById
{
    public class GetByIdUserQuery : IRequest<UserResultWithAddress?>
    {
        public int Id { get; set; }
    }
}