using ReportService.Application.Blogs.Commands.CreateBlog;
using ReportService.Application.Blogs.Commands.UpdateBlog;
using ReportService.Application.Blogs.Common;
using ReportService.Application.Users.Commands.CreateUser;
using ReportService.Application.Users.Commands.UpdateUser;
using ReportService.Application.Users.Common;
using ReportService.Domain.Blogs;
using ReportService.Domain.Users;

namespace ReportService.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UserResult>();
            CreateMap<User, UserResultWithAddress>();

            CreateMap<CreateBlogCommand, Blog>();
            CreateMap<UpdateBlogCommand, Blog>();
            CreateMap<Blog, BlogResult>();

            //CreateMap<Blog, GetBlogResponseDto>().ReverseMap()
            //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
        }
    }
}