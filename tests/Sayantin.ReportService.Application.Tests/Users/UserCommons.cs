using ReportService.Application.Users.Queries.GetAll;

namespace ReportService.Application.Tests.Users
{
    public static class UserCommons
    {
        public static async Task<UserResult> CreateUserAsync(this ISender sender, CreateUserCommand? createUserCommand = null)
        {
            createUserCommand ??= new CreateUserCommand() { Name = "TestUser", Birthday = new DateOnly(2000, 1, 1), Email = "test@gmail.com" };

            var userResult = await sender.Send(createUserCommand);

            userResult.Should().NotBeNull();
            userResult.Name.Should().Be(createUserCommand.Name);
            userResult.Birthday.Should().HaveYear(createUserCommand.Birthday.Year);
            userResult.Birthday.Should().HaveMonth(createUserCommand.Birthday.Month);
            userResult.Birthday.Should().HaveDay(createUserCommand.Birthday.Day);
            userResult.Email.Should().Be(createUserCommand.Email);

            return userResult;
        }

        public static async Task<List<UserResultWithAddress>> GetAllUsersAsync(this ISender sender)
        {
            var userResultWithAddressesList = await sender.Send(new GetAllUsersQuery());

            userResultWithAddressesList.Should().NotBeNull();

            return userResultWithAddressesList;
        }
    }
}
