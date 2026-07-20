using ReportService.Application.Exceptions;
using ReportService.Application.Users.Commands.DeleteUser;

namespace ReportService.Application.Tests.Users.Commands.DeleteUser
{
    public class DeleteUserTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task DeleteUser_ValidUserId_DeletesSuccessfully()
        {
            // Arrange
            var userResult = await Sender.CreateUserAsync();
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            var result = await Sender.Send(new DeleteUserCommand { UserId = userResult.Id });

            // Assert
            result.Should().BeTrue();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count - 1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public async Task DeleteUser_InvalidUserId_ThrowsValidationException(int invalidId)
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(new DeleteUserCommand { UserId = invalidId });

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }

        [Fact]
        public async Task DeleteUser_NonExistentUser_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(new DeleteUserCommand { UserId = int.MaxValue });

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }
    }
}