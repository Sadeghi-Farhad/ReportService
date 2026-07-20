using ReportService.Application.Blogs.Commands.CreateBlog;
using ReportService.Application.Blogs.Queries.GetAll;

namespace ReportService.Application.Tests.Blogs.Queries.GetAllBlog
{
    public class GetAllBlogsTests(TestingWebAppFactory factory) : TestingBase(factory)
    {
        [Fact]
        public async Task GetAllBlogs_WithCreatingBlogs_ReturnAll()
        {
            // Arrange
            await Sender.CreateBlogAsync(new CreateBlogCommand { Title = "A", Description = "DescA", AuthorId = 1 });
            await Sender.CreateBlogAsync(new CreateBlogCommand { Title = "B", Description = "DescB", AuthorId = 2 });

            // Act
            var blogResultList = await Sender.Send(new GetAllBlogsQuery());

            // Assert
            blogResultList.Select(b => b.Title).Should().Contain(["A", "B"]);
            blogResultList.Count.Should().Be(2);
        }

        [Fact]
        public async Task GetAllBlogs_WithNoBlogs_ReturnEmptyList()
        {
            // Act
            var result = await Sender.Send(new GetAllBlogsQuery());

            // Assert
            result.Should().BeEmpty();

        }
    }
}