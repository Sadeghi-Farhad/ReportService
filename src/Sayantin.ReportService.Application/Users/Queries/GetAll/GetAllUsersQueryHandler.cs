using ReportService.Application.Users.Common;
using ReportService.Domain.Users;

namespace ReportService.Application.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper)
        : IRequestHandler<GetAllUsersQuery, List<UserResultWithAddress>>
    {
        public async Task<List<UserResultWithAddress>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.ListAsync(cancellationToken);
            return mapper.Map<List<UserResultWithAddress>>(result);
        }
    }
}