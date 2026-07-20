using ReportService.Domain.Users;
using ReportService.Infrastructure.Data.Configuration;

namespace ReportService.Infrastructure.Repositories
{
    public class UserRepository(EFDbContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}