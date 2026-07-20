using ReportService.Application.Blogs.Commands.CreateBlog;
using ReportService.Application.Blogs.Commands.DeleteBlog;
using ReportService.Application.Blogs.Commands.PublishBlog;
using ReportService.Application.Blogs.Commands.UpdateBlog;
using ReportService.Application.Blogs.Common;
using ReportService.Application.Blogs.Queries.GetAll;
using ReportService.Application.Blogs.Queries.GetByAuthorId;
using ReportService.Application.Blogs.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : BaseController
    {
        /// <summary>
        /// ایجاد بلاگ جدید
        /// </summary>
        /// <param name="command">مشخصات بلاگ</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "title": "بهترین زبان برنامه نویسی دنیا",
        ///         "description": "این مقاله به بررسی زبانهای برنامه نویسی دنیا و انتخاب بهترین آنها می پردازد",
        ///         "authorId": 1
        ///     }
        /// </remarks>
        /// <response code="201">اطلاعات بلاگ ثبت شده</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpPost]
        [ProducesResponseType<BlogResult>(StatusCodes.Status201Created)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateBlogCommand command)
        {
            var result = await Sender.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// ویرایش اطلاعات یک بلاگ
        /// </summary>
        /// <param name="command">اطلاعات جدید بلاگ برای ویرایش</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "id": 123,
        ///         "title": "بهترین زبان برنامه نویسی دنیا",
        ///         "description": "این مقاله به بررسی زبانهای برنامه نویسی دنیا و انتخاب بهترین آنها می پردازد",
        ///         "authorId": 1
        ///     }
        /// </remarks>
        /// <response code="200">اطلاعات بلاگ پس از ویرایش</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpPut]
        [ProducesResponseType<BlogResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateBlogCommand command)
        {
            var result = await Sender.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف بلاگ
        /// </summary>
        /// <param name="id">شناسه بلاگ</param>
        /// <response code="200">انجام شدن یا نشدن عملیات حذف</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpDelete("{id}")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Sender.Send(new DeleteBlogCommand { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// انتشار یک بلاگ
        /// </summary>
        /// <param name="id">شناسه بلاگی که باید منتشر شود</param>
        /// <response code="200">انجام شدن یا نشدن عملیات انتشار</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpPost("{id}")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Publish(int id)
        {
            var result = await Sender.Send(new PublishBlogCommand { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// گرفتن لیست تمام بلاگ ها
        /// </summary>
        /// <response code="200">لیست تمام بلاگ ها</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpGet]
        [ProducesResponseType<List<BlogResult>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await Sender.Send(new GetAllBlogsQuery());
            return Ok(blogs);
        }

        /// <summary>
        /// گرفتن اطلاعات یک بلاگ بر اساس شناسه آن
        /// </summary>
        /// <param name="id">شناسه بلاگ</param>
        /// <response code="200">اطلاعات بلاگ</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpGet("{id}")]
        [ProducesResponseType<BlogResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await Sender.Send(new GetByIdBlogQuery { Id = id });
            return Ok(blog);
        }

        /// <summary>
        /// گرفتن لیست بلاگ های یک کاربر
        /// </summary>
        /// <param name="authorId">شناسه کاربر نویسنده بلاگ</param>
        /// <response code="200">لیست بلاگ های کاربر</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpGet("{autorId}")]
        [ProducesResponseType<List<BlogResult>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAuthorId(int authorId)
        {
            var lstblog = await Sender.Send(new GetByAuthorIdQuery { AuthorId = authorId });
            return Ok(lstblog);
        }
    }
}