using ReportService.Application.Blogs.Commands.UpdateBlog;
using ReportService.Application.Blogs.Queries.GetById;
using ReportService.Application.Exceptions;

namespace ReportService.Application.Tests.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task UpdateBlog_ValidInput_UpdatesSuccessfully()
        {
            // Arrange
            var blogResultList = await Sender.GetAllBlogsAsync();

            var blogResult = await Sender.CreateBlogAsync();

            var updateBlogCommand = new UpdateBlogCommand
            {
                Id = blogResult.Id,
                Title = "UpdatedTitle",
                Description = "UpdatedDescription",
                AuthorId = blogResult.AuthorId
            };

            // Act
            await Sender.Send(updateBlogCommand);

            var getByIdBlogQuery = new GetByIdBlogQuery { Id = blogResult.Id };
            blogResult = await Sender.Send(getByIdBlogQuery);

            // Assert
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count);
            blogResult.Should().NotBeNull();
            blogResult.Id.Should().Be(updateBlogCommand.Id);
            blogResult.Title.Should().Be(updateBlogCommand.Title);
            blogResult.Description.Should().Be(updateBlogCommand.Description);
        }

        [Theory]
        [InlineData(0, "Title", "Desc", 1)]
        [InlineData(1, "", "Desc", 1)]
        [InlineData(1, "Title", "", 1)]
        [InlineData(1, "Title", "Desc", 0)]
        [InlineData(1, "Title", "moreThan500Char", 1)]
        public async Task UpdateBlog_InvalidInput_ThrowsValidationException(int id, string title, string description, int authorId)
        {
            // Arrange
            var updateBlogCommand = new UpdateBlogCommand
            {
                Id = id,
                Title = title,
                Description = description,
                AuthorId = authorId
            };

            if (description == "moreThan500Char")
            {
                updateBlogCommand.Description = new string('x', 501);
            }

            var blogLists = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(updateBlogCommand);

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogLists.Count);
        }

        [Fact]
        public async Task UpdateBlog_NonExistentBlog_ThrowsKeyNotFoundException()
        {
            // Arrange
            var updateBlogCommand = new UpdateBlogCommand
            {
                Id = int.MaxValue,
                Title = "Guest",
                Description = "NoBody",
                AuthorId = 1
            };
            var blogLists = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(updateBlogCommand);

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogLists.Count);
        }
    }
}