using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SimplifiedPicPay;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage, string title) = exception switch
        {
            BadHttpRequestException badHttpRequestException => (StatusCodes.Status400BadRequest, badHttpRequestException.Message, "Bad Request"),
            UnauthorizedAccessException unauthorizedAccessException => (StatusCodes.Status401Unauthorized, unauthorizedAccessException.Message, "Unauthorized"),
            HttpRequestException httpRequestException => (StatusCodes.Status500InternalServerError, httpRequestException.Message, "Internal Server Error"),
            _ => (500, "Something went wrong", "Internal Error")
        };

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = errorMessage
        };
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}