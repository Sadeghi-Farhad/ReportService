using ReportService.Application.Exceptions;
using ReportService.Application.Users.Queries.GetById;

namespace ReportService.Application.Tests.Users.Queries.GetById
{
    public class GetByIdUserTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task GetByIdUser_ValidId_ReturnsUser()
        {
            // Arrange
            var userResult = await Sender.CreateUserAsync();

            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            var userResultWithAddress = await Sender.Send(new GetByIdUserQuery { Id = userResult.Id });

            // Result
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
            userResultWithAddress.Should().NotBeNull();
            userResultWithAddress.Id.Should().Be(userResult.Id);
            userResultWithAddress.Name.Should().Be(userResult.Name);
        }

        [Fact]
        public async Task GetByIdUser_InvalidId_ThrowsValidationException()
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(new GetByIdUserQuery { Id = 0 });

            // Result
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
            await result.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task GetByIdUser_UserNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            var result = await Sender.Send(new GetByIdUserQuery { Id = int.MaxValue });

            // Result
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
            result.Should().BeNull();
        }
    }
}