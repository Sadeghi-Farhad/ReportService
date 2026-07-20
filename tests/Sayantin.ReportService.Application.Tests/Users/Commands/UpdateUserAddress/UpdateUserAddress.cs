using ReportService.Application.Exceptions;
using ReportService.Application.Users.Commands.UpdateUser;
using ReportService.Application.Users.Queries.GetById;

namespace ReportService.Application.Tests.Users.Commands.UpdateUserAddress
{
    public class UpdateUserAddressTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task UpdateUserAddress_ValidData_UpdateAddressSuccessfully()
        {
            // Arrange
            var userResult = await Sender.CreateUserAsync();
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();

            // Act
            var updateUserAddressCommand = new UpdateUserAddressCommand
            {
                UserId = userResult.Id,
                Province = "Province",
                City = "City",
                Street = "Street",
                PostalCode = "PostalCode"
            };

            // Act
            await Sender.Send(updateUserAddressCommand);

            var userResultWithAddress = await Sender.Send(new GetByIdUserQuery { Id = userResult.Id });

            // Assert
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
            userResultWithAddress.Should().NotBeNull();
            userResultWithAddress.Address.Should().NotBeNull();
            userResultWithAddress.Address.City.Should().Be(updateUserAddressCommand.City);
            userResultWithAddress.Address.PostalCode.Should().Be(updateUserAddressCommand.PostalCode);
            userResultWithAddress.Address.Street.Should().Be(updateUserAddressCommand.Street);
            userResultWithAddress.Address.Province.Should().Be(updateUserAddressCommand.Province);
        }

        [Theory]
        [InlineData(0, "Province", "City", "Street", "PostalCode")]
        [InlineData(-1, "Province", "City", "Street", "PostalCode")]
        [InlineData(1, null, "City", "Street", "PostalCode")]
        [InlineData(0, "Province", null, "Street", "PostalCode")]
        [InlineData(0, "Province", "City", null, "PostalCode")]
        [InlineData(0, "Province", "City", "Street", null)]
        public async Task UpdateUserAddress_InvalidData_ThrowValidationException(int Id, string? Province, string? City, string? Street, string? PostalCode)
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();
            var updateUserAddressCommand = new UpdateUserAddressCommand
            {
                UserId = Id,
                Province = Province,
                City = City,
                Street = Street,
                PostalCode = PostalCode
            };

            // Act
            Func<Task> result = () => Sender.Send(updateUserAddressCommand);

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);

        }

        [Fact]
        public async Task UpdateUserAddress_NotExistUser_ThrowKeyNotFoundException()
        {
            // Arrange
            var userResultWithAddressesList = await Sender.GetAllUsersAsync();
            var updateUserAddressCommand = new UpdateUserAddressCommand
            {
                UserId = int.MaxValue,
                Province = "Province",
                City = "City",
                Street = "Street",
                PostalCode = "PostalCode"
            };

            // Act
            Func<Task> result = () => Sender.Send(updateUserAddressCommand);

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(userResultWithAddressesList.Count);
        }

    }
}