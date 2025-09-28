using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RentalAgency.CustomExceptions.NotFoundExceptions;
using RentalAgency.CustomExceptions.ValidtionExceptions;

namespace RentalAgency.Api.ExceptionHandling;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
        var path = context.HttpContext.Request.Path;
        
        var (title, statusCode, detail) = exception switch
        {
            ItemNotFoundException notFound => 
                (
                    notFound.Message, 
                    StatusCodes.Status404NotFound,
                    "Item with current id was not found"
                    ),
            EntityValidationException entityValidation => 
                (
                    entityValidation.Message,
                    StatusCodes.Status400BadRequest,
                    "Validation failed"
                    )
        };

        ProblemDetails problemDetails = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = detail,
            Instance = path
        };
        
        problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Extensions["path"] = path;

        _logger.LogError(exception, "Caught exception: {message}", exception.Message);
        
        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }
}