using ReportService.Application.Exceptions;
namespace ReportService.Application.Tests.Users.Commands.CreateUser
{
    public class CreateUserTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task CreateUser_ValidInput_AddsSuccessfully()
        {
            // Arrange
            var createUserCommand = new CreateUserCommand
            {
                Name = "Crouse",
                Birthday = DateOnly.Parse("1980-10-01"),
                Email = "nietzsche@errorfix.com"
            };
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            var userResult = await Sender.Send(createUserCommand);

            // Assert
            userResult.Should().NotBeNull();
            userResult.Name.Should().Be("Crouse");
            userResult.Email.Should().Be("nietzsche@errorfix.com");
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count + 1);
        }

        [Theory]
        [InlineData("1", "1980-10-01", null)]
        [InlineData("1", null, "nietzsche@errorfix.com")]
        [InlineData(null, "1980-10-02", "nietzsche@errorfix.com")]
        [InlineData(null, "1980-10-03", "nietzsche@errorfix")]
        [InlineData(null, "1980-10-04", ".com")]
        [InlineData("12", "1980-10-06", "nietzsche.com")]
        public async Task CreateUser_InvalidInput_ThrowValidationException(string? name, string? date, string? email)
        {
            // Arrange
            var createUserCommand = new CreateUserCommand
            {
                Name = name,
                Email = email
            };

            if (date != null)
                createUserCommand.Birthday = DateOnly.Parse(date);

            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(createUserCommand);

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }
    }
}