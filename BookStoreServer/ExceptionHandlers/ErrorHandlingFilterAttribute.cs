using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.ExceptionHandlers;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var problemDetails = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path
        };

        switch (exception)
        {
            case DbUpdateException:
                problemDetails.Status = 400;
                problemDetails.Title = exception.InnerException?.Message ?? exception.Message;
                break;
            case InvalidDataException:
                problemDetails.Status = 400;
                problemDetails.Title = exception.Message;
                break;
            default:
                problemDetails.Status = 500;
                problemDetails.Title = "Unexpected error occurred";
                break;
        }
        
        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}