using Bl.DataAPI;
using Dal.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
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
    public IActionResult DevelopmentError(/*[FromServices] IHostEnvironment hostEnvironment*/)
    {
        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>();
        _logger.LogError(exceptionHandlerFeature?.Error.ToString());
        //var a = new ProblemDetails();
        if (exceptionHandlerFeature?.Error is NotExistsDataObjectException)
        { 

           // return BadRequest(exceptionHandlerFeature.Error.Message);
        }
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error; // Your exception
        var code = 500; // Internal Server Error by default

        // if (exception is NotExistsDataObjectException) code = 400; // Not Found
        //else if (exception is MyUnauthException) code = 401; // Unauthorized
        //else if (exception is MyException) code = 400; // Bad Request

        //  Response.StatusCode = code; // You can use HttpStatusCode enum instead
        //ServiceResponseException srex = exception.InnerException as ServiceResponseException
        var a = exception as GlobalException;
        return Problem(
            detail: context?.Error.StackTrace,
            title: context?.Error.Message,
            statusCode:  a.Status// exception?.HResult//need to search s solution to use code without using Dal.Exceptions;!!!
            );
            
        
            
    }
}
public class MyErrorResponse
{
    public string Type { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }

    public MyErrorResponse(Exception ex)
    {
        Type = ex.GetType().Name;
        Message = ex.Message;
        StackTrace = ex.ToString();
    }
}
public class HttpStatusException : Exception
{
    public HttpStatusCode Status { get; private set; }

    public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
    {
        Status = status;
    }
}

