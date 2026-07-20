using ReportService.Application.Users.Queries.GetAll;

namespace ReportService.Application.Tests.Users.Queries.GetAllUser
{
    public class GetAllUsersTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task GetAllUsers_ReturnAllUsers()
        {
            // Arrange
            await Sender.CreateUserAsync(new CreateUserCommand() { Name = "User1", Birthday = new DateOnly(2000, 1, 1), Email = "user1@gmail.com" });
            await Sender.CreateUserAsync(new CreateUserCommand() { Name = "User2", Birthday = new DateOnly(2000, 1, 2), Email = "user2@gmail.com" });
            await Sender.CreateUserAsync(new CreateUserCommand() { Name = "User3", Birthday = new DateOnly(2000, 1, 3), Email = "user3@gmail.com" });

            // Act
            var userResultWithAddressesList = await Sender.Send(new GetAllUsersQuery());

            // Assert
            userResultWithAddressesList.Should().NotBeNull();
            userResultWithAddressesList.Should().HaveCount(3);

            foreach (var user in userResultWithAddressesList)
            {
                user.Id.Should().BeGreaterThan(0);
                user.Name.Should().NotBeNullOrWhiteSpace();
            }
        }

        [Fact]
        public async Task GetAllUsers_NoUsers_ReturnEmptyList()
        {
            // Act
            var userResultWithAddressesList = await Sender.Send(new GetAllUsersQuery());

            // Assert
            userResultWithAddressesList.Should().NotBeNull();
            userResultWithAddressesList.Should().BeEmpty();
        }
    }
}