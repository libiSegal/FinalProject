using Dal.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Http.Results;
//using System.Web.Http;
namespace UI.Controllers;

//[Route("api/[controller]")]
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
        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>();
        _logger.LogError(exceptionHandlerFeature?.Error.ToString());

        return Problem(
            detail: "Please try later...",
            title: "Ooops...");
    }
    [Route("/error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult DevelopmentError([FromServices] IHostEnvironment hostEnvironment)
    {
        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>();
        _logger.LogError(exceptionHandlerFeature?.Error.ToString());
        if(exceptionHandlerFeature?.Error is NotExistsDataObjectException)
        {
            return BadRequest(exceptionHandlerFeature.Error.Message);
        }
        return Problem(
            detail: exceptionHandlerFeature?.Error.StackTrace,
            title: exceptionHandlerFeature?.Error.Message);

    }
}

