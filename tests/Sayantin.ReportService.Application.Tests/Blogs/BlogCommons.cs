using ReportService.Application.Blogs.Commands.CreateBlog;
using ReportService.Application.Blogs.Common;
using ReportService.Application.Blogs.Queries.GetAll;
using ReportService.Application.Tests.Users;

namespace ReportService.Application.Tests.Blogs
{
    public static class BlogCommons
    {
        public static async Task<BlogResult> CreateBlogAsync(this ISender sender, CreateBlogCommand? createBlogCommand = null)
        {
            var userResult = await sender.CreateUserAsync();
            createBlogCommand ??= new CreateBlogCommand { Title = "TestTitle", Description = "TestDescription", AuthorId = userResult.Id };

            var blogResult = await sender.Send(createBlogCommand);

            blogResult.Should().NotBeNull();
            blogResult.Title.Should().Be(createBlogCommand.Title);
            blogResult.Description.Should().Be(createBlogCommand.Description);
            blogResult.AuthorId.Should().Be(createBlogCommand.AuthorId);
            return blogResult;
        }

        public static async Task<List<BlogResult>> GetAllBlogsAsync(this ISender sender)
        {
            var blogResultList = await sender.Send(new GetAllBlogsQuery());

            blogResultList.Should().NotBeNull();

            return blogResultList;
        }
    }
}