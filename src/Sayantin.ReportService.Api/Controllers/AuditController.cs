using ReportService.Application.Audit.Queries.GetByParentId;
using ReportService.Application.Blogs.Common;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditController : BaseController
    {
        /// <summary>
        /// گرفتن اطلاعات آدیت یک سند
        /// </summary>
        /// <param name="id">شناسه سند</param>
        /// <response code="200">اطلاعات آدیت</response>
        /// <response code="400">داده‌های ورودی نامعتبر هستند</response>
        /// <response code="500">خطای داخلی سرور</response>
        [HttpGet("{id}")]
        [ProducesResponseType<BlogResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var auditResults = await Sender.Send(new GetByParentIdQuery { ParentId = id });
            return Ok(auditResults);
        }
    }
}
