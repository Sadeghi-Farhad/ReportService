using ReportService.Application.Blogs.Commands.CreateBlog;
using ReportService.Application.Exceptions;
using ReportService.Application.Tests.Users;

namespace ReportService.Application.Tests.Blogs.Commands.CreateBlog
{
    public class CreateBlogTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task CreateBlog_ValidInput_AddsSuccessfully()
        {
            // Arrange
            var userResult = await Sender.CreateUserAsync();

            var createBlogCommand = new CreateBlogCommand
            {
                Title = "NewPost",
                Description = "Integration Testing Rocks",
                AuthorId = userResult.Id
            };

            var blogResultList = await Sender.GetAllBlogsAsync();

            // Act
            var blogResult = await Sender.Send(createBlogCommand);

            // Assert
            blogResult.Should().NotBeNull();
            blogResult.Title.Should().Be(createBlogCommand.Title);
            blogResult.Description.Should().Be(createBlogCommand.Description);
            blogResult.AuthorId.Should().Be(createBlogCommand.AuthorId);
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count + 1);
        }

        [Theory]
        [InlineData("", "Desc", 1)]
        [InlineData("Title", "", 1)]
        [InlineData("Title", "Desc", 0)]
        [InlineData("Title", "moreThan500Char", 1)]
        public async Task CreateBlog_InvalidInput_ThrowsValidationException(string title, string description, int authorId)
        {
            // Arrange
            var createBlogCommand = new CreateBlogCommand
            {
                Title = title,
                Description = description,
                AuthorId = authorId
            };
            if (description == "moreThan500Char")
            {
                createBlogCommand.Description = new string('x', 501);
            }

            var blogResultList = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(createBlogCommand);

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count);
        }
    }
}