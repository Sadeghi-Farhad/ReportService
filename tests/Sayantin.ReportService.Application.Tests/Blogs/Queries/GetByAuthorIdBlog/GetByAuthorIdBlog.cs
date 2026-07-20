using ReportService.Application.Blogs.Commands.CreateBlog;
using ReportService.Application.Blogs.Queries.GetByAuthorId;
using ReportService.Application.Exceptions;

namespace ReportService.Application.Tests.Blogs.Queries.GetByAuthorIdBlog
{
    public class GetByAuthorIdTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task GetByAuthorId_ValidAuthorId_ReturnsOnlyThatAuthorsBlogs()
        {
            // Arrange
            var blogResultList = await Sender.GetAllBlogsAsync();

            await Sender.CreateBlogAsync(new CreateBlogCommand { Title = "A1", Description = "DescA1", AuthorId = 1 });
            await Sender.CreateBlogAsync(new CreateBlogCommand { Title = "A2", Description = "DescA2", AuthorId = 1 });
            await Sender.CreateBlogAsync(new CreateBlogCommand { Title = "B1", Description = "DescB1", AuthorId = 2 });

            // Act
            var blogResultListGroupByAuthorId = await Sender.Send(new GetByAuthorIdQuery { AuthorId = 1 });

            // Assert
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count + 3);
            blogResultListGroupByAuthorId.Should().NotBeNull();
            blogResultListGroupByAuthorId.Should().OnlyContain(b => b.AuthorId == 1);
            blogResultListGroupByAuthorId.Should().HaveCount(2);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public async Task GetByAuthorId_InvalidAuthorId_ThrowsValidationException(int invalidId)
        {
            // Arrange
            var blogResultList = await Sender.GetAllBlogsAsync();

            // Act
            Func<Task> result = () => Sender.Send(new GetByAuthorIdQuery { AuthorId = invalidId });

            // Assert
            await result
                .Should()
                .ThrowAsync<ValidationException>();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count);
        }

        [Fact]
        public async Task GetByAuthorId_NoBlogsForAuthor_ReturnsEmptyList()
        {
            // Arrange
            var blogResultList = await Sender.GetAllBlogsAsync();

            // Act
            var result = await Sender.Send(new GetByAuthorIdQuery { AuthorId = int.MaxValue });

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            (await Sender.GetAllBlogsAsync()).Count.Should().Be(blogResultList.Count);
        }
    }
}