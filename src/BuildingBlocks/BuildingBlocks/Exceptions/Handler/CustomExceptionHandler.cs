using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);

        (string Detail, string Title, int StatusCode) = exception switch
        {
            ValidationException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            _ => (
                "An error occurred while processing your request.",
                "An error occurred.",
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };
        
        var problemDetails = new ProblemDetails
        {
            Detail = Detail,
            Title = Title,
            Status = StatusCode,
            Instance = context.Request.Path
        };
        
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}