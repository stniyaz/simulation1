using System.Net;

namespace Hackathon.Api.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Xəta baş verdi: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Daxili server xətası baş verdi.";

        if (exception is Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        var errorResponse = new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        };

        await context.Response.WriteAsync(errorResponse.ToString());
    }
}
