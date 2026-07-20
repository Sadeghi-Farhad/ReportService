using ReportService.Application.Users.Common;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Users;

namespace ReportService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateUserCommand, UserResult>
    {
        public async Task<UserResult> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var user = mapper.Map<User>(request);
            var result = await repository.UpdateAsync(user);
            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<UserResult>(result);
        }
    }
}