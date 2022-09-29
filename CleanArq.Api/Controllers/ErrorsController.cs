using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArq.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult ExceptionError()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var message = exception?.Message ?? exception?.InnerException?.Message;

        return Problem(message);
    }

    [Route("/error/{code:int}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult ReExecutedPage(int? code)
    {
        return Problem(statusCode: code);
    }
}