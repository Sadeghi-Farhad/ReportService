using ReportService.Application.Exceptions;
using ReportService.Application.Users.Commands.UpdateUser;

namespace ReportService.Application.Tests.Users.Commands.UpdateUser
{
    public class UpdateUserTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task UpdateUser_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();
            var userResult = await Sender.CreateUserAsync();
            var updateUserCommand = new UpdateUserCommand
            {
                Id = userResult.Id,
                Name = "NewName",
                Birthday = new DateOnly(2010, 10, 10),
                Email = "new@email.com"
            };

            // Act
            userResult = await Sender.Send(updateUserCommand);

            // Assert
            userResult.Name.Should().Be("NewName");
            userResult.Birthday.Should().Be(new DateOnly(2010, 10, 10));
            userResult.Email.Should().Be("new@email.com");
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }

        [Theory]
        [InlineData(0, "ValidName", "2000-01-01", "valid@email.com")]
        [InlineData(1, "", "2000-01-01", "valid@email.com")]
        [InlineData(2, "ValidName", "2000-01-01", "")]
        [InlineData(2, "ValidName", null, "valid@email.com")]
        public async Task UpdateUser_InvalidInput_ThrowsValidationException(
            int userId, string name, string? birthday, string email)
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();
            var updateUserCommand = new UpdateUserCommand
            {
                Id = userId,
                Name = name,
                Email = email
            };
            if (birthday != null)
                updateUserCommand.Birthday = DateOnly.Parse(birthday);

            // Act
            Func<Task> result = () => Sender.Send(updateUserCommand);

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }

        [Fact]
        public async Task UpdateUser_NonExistentUser_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();
            var updateUserCommand = new UpdateUserCommand
            {
                Id = int.MaxValue,
                Name = "GhostUser",
                Birthday = new DateOnly(1990, 1, 1),
                Email = "ghost@email.com"
            };

            // Act
            Func<Task> result = () => Sender.Send(updateUserCommand);

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }
    }
}