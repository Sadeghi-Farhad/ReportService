using ReportService.Application.Blogs.Commands.PublishBlog;
using ReportService.Application.Exceptions;
using ReportService.Application.Tests.Users;
using Xunit.Abstractions;

namespace ReportService.Application.Tests.Blogs.Commands.PublishBlog
{
    public class PublishBlogTests(TestingWebAppFactory factory, ITestOutputHelper outputHelper) : TestingBaseWithMoqEmailService(factory, outputHelper)
    {

        [Fact]
        public async Task PublishBlog_ValidId_ReturnsTrue()
        {
            // Arrange
            var blogResultList = await Sender.GetAllUsersAsync();

            var blogResult = await Sender.CreateBlogAsync();

            // Act
            var result = await Sender.Send(new PublishBlogCommand { Id = blogResult.Id });

            // Assert
            result.Should().BeTrue();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(blogResultList.Count + 1);
        }

        [Fact]
        public async Task PublishBlog_InvalidId_ThrowsValidationException()
        {
            // Arrange
            var blogResultList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(new PublishBlogCommand { Id = 0 });

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(blogResultList.Count);
        }

        [Fact]
        public async Task PublishBlog_NonExistentBlog_ThrowsKeyNotFoundException()
        {
            // Arrange
            var blogResultList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(new PublishBlogCommand { Id = int.MaxValue });

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(blogResultList.Count);
        }
    }
}
