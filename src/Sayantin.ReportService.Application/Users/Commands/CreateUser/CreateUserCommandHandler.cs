using ReportService.Application.Users.Common;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Users;

namespace ReportService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateUserCommand, UserResult>
    {
        public async Task<UserResult> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var user = mapper.Map<User>(request);
            var createdUser = await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<UserResult>(createdUser);
        }
    }
}