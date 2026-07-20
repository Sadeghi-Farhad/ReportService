using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Controllers
{
    [ApiController]
    public class DocController : ControllerBase
    {
        /// <summary>
        /// محتوای فایل راهنما را بصورت متنی برمی گرداند.
        /// </summary>
        /// <param name="file" example="readme.md">نام فایل</param>
        /// <returns>محتوای فایل</returns>
        [HttpGet]
        [Route("[controller]")]
        [ProducesResponseType<string>(200)]
        [ProducesResponseType<string>(404)]
        public async Task<IActionResult> Index(string file)
        {
            var safeName = Path.GetFileName(file);
            if (string.IsNullOrWhiteSpace(safeName))
                return BadRequest("invalid file name");

            var docPath = $"docs/{safeName}";
            if (!System.IO.File.Exists(docPath))
                return NotFound($"file not found: {file}");

            var content = await System.IO.File.ReadAllTextAsync(docPath);
            var result = safeName.EndsWith(".md", true, CultureInfo.CurrentCulture)
                ? Markdig.Markdown.ToHtml(content)
                : content;

            // Set content type with charset=utf-8 and serve as HTML

            return Content(result, "text/html; charset=utf-8");
        }
    }
}