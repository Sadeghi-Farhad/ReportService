using ReportService.Application.Users.Commands.UpdateUser;
using ReportService.Application.Users.Common;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Users;

namespace ReportService.Application.Users.Commands.UpdateUserAddress
{
    public class UpdateUserAddressCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserAddressCommand, UserResultWithAddress>
    {
        public async Task<UserResultWithAddress> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new Domain.Exceptions.KeyNotFoundException("کاربر یافت نشد.");

            user.SetAddress(request.Province, request.City, request.Street, request.PostalCode);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<UserResultWithAddress>(user);
        }
    }
}