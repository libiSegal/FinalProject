using Bl.DataAPI;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace UI.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    ILogger<ErrorsController> _logger;
    public ErrorsController(ILogger<ErrorsController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error([FromServices] IHostEnvironment hostEnvironment)
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        _logger.LogError(exception?.ToString());

        var blException = exception as BLException;
        return Problem(
            title: "Ooops, Something wrong",
            detail: exception?.Message,
            statusCode : blException?.Status
            );
    }
    [Route("/error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult DevelopmentError([FromServices] IHostEnvironment hostEnvironment)
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        _logger.LogError(exception?.ToString());
        
        var blException = exception as BLException;
        return Problem(
            title: context?.Error.Message,
            detail: context?.Error.StackTrace,
            statusCode:  blException?.Status
            );     
    }
}