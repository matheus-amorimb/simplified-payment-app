using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PicPayBackendChallenge.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _request;

    public ExceptionHandlingMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code;
        string result;
        
        switch (exception)
        {
            case ArgumentException e:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { error = e.Message });
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new { error = exception.Message });
                break;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        
        return context.Response.WriteAsync(result);
    }
}