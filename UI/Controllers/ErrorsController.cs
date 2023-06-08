﻿
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
        if (blException != null)
        {
            if (blException.Status > 399 && blException.Status < 500)
            {
                return Problem(
                /*title: "Ooops, You have a wrong...",*/
                title: blException.Message,
                statusCode: blException.Status);
            }
            else
            {
                if (blException.Status == 400 || blException.Status == 404)
                return Problem(
                statusCode: blException.Status);
            }
        }
        return Problem(
            title: "Ooops...",
            detail: "Please try later...");  
    }
    [Route("/error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult DevelopmentError([FromServices] IHostEnvironment hostEnvironment)
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        _logger.LogError(exception?.ToString());
        var blException = exception as BLException;
        if(blException != null)
        {
            return Problem(
                title: context?.Error.Message,
                detail: context?.Error.StackTrace,
                statusCode:  blException?.Status
                );
        }
        return Problem(
                title: context?.Error.Message,
                detail: context?.Error.StackTrace,
                statusCode: 500
            );      
    }
}