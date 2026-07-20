using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Controllers
{
    /// <summary>
    /// Represents the base API controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private ISender? _sender;
        private IMapper? _mapper;

        /// <summary>
        /// Gets the sender.
        /// </summary>
        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}