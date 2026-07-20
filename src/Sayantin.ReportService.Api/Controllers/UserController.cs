using ReportService.Application.Users.Commands.CreateUser;
using ReportService.Application.Users.Commands.DeleteUser;
using ReportService.Application.Users.Commands.UpdateUser;
using ReportService.Application.Users.Common;
using ReportService.Application.Users.Queries.GetAll;
using ReportService.Application.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        /// <summary>
        /// ایجاد کاربر جدید
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "name": "علی محمدی",
        ///         "birthday": "2025-07-28",
        ///         "email": "sample@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="command">مشخصات کاربر جدید</param>
        /// <response code="201">اطلاعات کاربر جدید پس از ثبت</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpPost]
        [ProducesResponseType<UserResult>(StatusCodes.Status201Created)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await Sender.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// ویرایش مشخصات کاربر
        /// </summary>
        /// <param name="command">مشخصات کاربر برای ویرایش</param>
        /// <remarks>
        /// Sample Request:
        ///
        ///     {
        ///         "id": 123,
        ///         "name": "علی محمدی",
        ///         "birthday": "2025-07-28",
        ///         "email": "sample@gmail.com"
        ///     }
        /// </remarks>
        /// <response code="200">مشخصات جدید کابر پس از ویرایش</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpPut]
        [ProducesResponseType<UserResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await Sender.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// ویرایش آدرس کاربر
        /// </summary>
        /// <param name="command">آدرس جدید کاربر</param>
        /// <remarks>
        /// Sample Request:
        ///
        ///     {
        ///         "UserId": 123,
        ///         "Province": "البرز",
        ///         "City": "کرج",
        ///         "Street": "گلستان یکم"
        ///         "PostalCode": "1234567890"
        ///     }
        /// </remarks>
        /// <response code="200">مشخصات جدید کابر بهمراه آدرس پس از ویرایش</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpPut]
        [ProducesResponseType<UserResultWithAddress>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateUserAddressCommand command)
        {
            var result = await Sender.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <param name="id">شناسه کاربر</param>
        /// <response code="200">انجام شدن یا نشدن عملیات حذف</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Sender.Send(new DeleteUserCommand { UserId = id });
            return Ok(result);
        }

        /// <summary>
        /// گرفتن لیست تمام کاربران
        /// </summary>
        /// <response code="200">لیست تمام کاربران</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpGet]
        [ProducesResponseType<IEnumerable<UserResult>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var users = await Sender.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        /// <summary>
        /// گرفتن اطلاعات کاربر با شناسه داده شده
        /// </summary>
        /// <param name="id">شناسه کاربر</param>
        /// <response code="200">اطلاعات کاربر</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpGet("{id}")]
        [ProducesResponseType<UserResultWithAddress>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await Sender.Send(new GetByIdUserQuery { Id = id });
            return Ok(blog);
        }
    }
}