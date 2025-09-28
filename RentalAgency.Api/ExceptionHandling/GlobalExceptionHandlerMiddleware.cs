using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RentalAgency.Api.ExceptionHandling;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            var path = context.Request.Path;
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            var problemDetails = new ProblemDetails
            {
                Detail = "Something went wrong",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected Error",
                Instance = path,
            };
            
            problemDetails.Extensions.Add("traceId", traceId);
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}