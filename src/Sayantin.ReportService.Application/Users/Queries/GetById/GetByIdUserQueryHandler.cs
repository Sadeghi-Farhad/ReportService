using ReportService.Application.Users.Common;
using ReportService.Domain.Users;

namespace ReportService.Application.Users.Queries.GetById
{
    public class GetByIdUserQueryHandler(IUserRepository repository, IMapper mapper)
        : IRequestHandler<GetByIdUserQuery, UserResultWithAddress?>
    {
        public async Task<UserResultWithAddress?> Handle(GetByIdUserQuery request, CancellationToken ct)
        {
            var user = await repository.GetByIdAsync(request.Id, ct);
            return mapper.Map<UserResultWithAddress?>(user);
        }
    }
}