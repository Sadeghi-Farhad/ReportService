using ReportService.Application.Blogs.Commands.DeleteBlog;
using ReportService.Application.Exceptions;
using ReportService.Application.Tests.Users;

namespace ReportService.Application.Tests.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task DeleteBlog_ValidId_DeletesSuccessfully()
        {
            // Arrange
            var blogResult = await Sender.CreateBlogAsync();

            var blogResultList = await Sender.GetAllBlogsAsync();

            // Act
            var result = await Sender.Send(new DeleteBlogCommand { Id = blogResult.Id });

            // Assert
            result.Should().BeTrue();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count - 1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task DeleteBlog_InvalidId_ThrowsValidationException(int blogId)
        {
            // Arrange
            var blogResultList = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(new DeleteBlogCommand { Id = blogId });

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count);
        }

        [Fact]
        public async Task DeleteBlog_NonExistentUser_ThrowsKeyNotFoundException()
        {
            // Arrange
            var blogResultList = await Sender.GetAllUsersAsync();

            // Act
            Func<Task> result = () => Sender.Send(new DeleteBlogCommand { Id = int.MaxValue });

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllUsersAsync()).Count.Should().Be(blogResultList.Count);
        }
    }
}