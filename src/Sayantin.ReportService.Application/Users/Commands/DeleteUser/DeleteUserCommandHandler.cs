using ReportService.Domain.Interfaces;
using ReportService.Domain.Users;

namespace ReportService.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken ct)
        {
            var user = await repository.GetByIdAsync(request.UserId);
            if (user == null) throw new Domain.Exceptions.KeyNotFoundException("کاربر یافت نشد.");

            var result = await repository.DeleteAsync(user);
            await unitOfWork.SaveChangesAsync(ct);

            return result;
        }
    }
}