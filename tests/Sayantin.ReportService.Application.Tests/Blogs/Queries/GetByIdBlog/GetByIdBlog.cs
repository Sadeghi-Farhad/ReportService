using ReportService.Application.Blogs.Queries.GetById;
using ReportService.Application.Exceptions;

namespace ReportService.Application.Tests.Blogs.Queries.GetByIdBlog
{
    public class GetByIdBlogTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task GetByIdBlog_ValidId_ReturnsBlog()
        {
            // Arrange
            var blogResult = await Sender.CreateBlogAsync();

            var blogLists = await Sender.GetAllBlogsAsync();

            // Act
            var blogResultFromQuery = await Sender.Send(new GetByIdBlogQuery { Id = blogResult.Id });

            // Assert
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogLists.Count);
            blogResultFromQuery.Should().NotBeNull();
            blogResultFromQuery.Id.Should().Be(blogResult.Id);
            blogResultFromQuery.Title.Should().Be(blogResult.Title);
            blogResultFromQuery.AuthorId.Should().Be(blogResult.AuthorId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetByIdBlog_InvalidId_ThrowsValidationException(int invalidId)
        {
            // Arrange
            var blogLists = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(new GetByIdBlogQuery { Id = invalidId });

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogLists.Count);
        }

        [Fact]
        public async Task GetByIdBlog_NonExistentBlog_ThrowsKeyNotFoundException()
        {
            // Arrange
            var blogLists = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(new GetByIdBlogQuery { Id = int.MaxValue });

            // Assert
            await result.Should().ThrowAsync<Domain.Exceptions.KeyNotFoundException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogLists.Count);
        }
    }
}