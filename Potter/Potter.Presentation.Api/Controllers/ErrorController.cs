using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Potter.Presentation.Api.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IActionResult Error() => Problem();

        [Route("local-development")]
        [HttpGet]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException("This shouldn't be invoked in non-development environments.");

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        }
    }
}
