using Hackathon.Business.Helpers.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security;
using System.Text.Json;

namespace Hackathon.Api.Middlewares;

public class GlobalHandleException
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalHandleException> _logger;

    public GlobalHandleException(RequestDelegate next, ILogger<GlobalHandleException> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogException(context, ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private void LogException(HttpContext context, Exception ex)
    {
        string? user = context.User.Identity?.Name ?? "Anonim";
        string? ip = context.Connection.RemoteIpAddress?.ToString();
        string? method = context.Request.Method;
        string path = context.Request.Path;
        string traceId = context.TraceIdentifier;

        _logger.LogError(ex,
            "❌ Xəta baş verdi! User: {User}, IP: {IP}, Path: {Path}, Method: {Method}, TraceId: {TraceId}, Message: {Message}, Inner: {Inner}",
            user, ip, path, method, traceId, ex.Message, ex.InnerException?.Message
        );
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var statusCode = exception switch
        {
            ArgumentException => (int)HttpStatusCode.BadRequest,
            EntityNotFoundException => (int)HttpStatusCode.NotFound,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            FileNotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            NotImplementedException => (int)HttpStatusCode.NotImplemented,
            TimeoutException => (int)HttpStatusCode.RequestTimeout,
            SecurityTokenExpiredException => (int)HttpStatusCode.Unauthorized,
            EntityExistException => (int)HttpStatusCode.Conflict,
            SecurityException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        response.StatusCode = statusCode;

        var userMessage = exception switch
        {
            ArgumentException => "Daxil edilən məlumat etibarsızdır.",
            EntityNotFoundException => "Tələb edilən məlumat tapılmadı.",
            KeyNotFoundException => "Açar tapılmadı.",
            FileNotFoundException => "Fayl mövcud deyil.",
            UnauthorizedAccessException => "Giriş icazəniz yoxdur.",
            InvalidOperationException => "Etibarsız əməliyyat.",
            NotImplementedException => "Bu funksiya hələ hazır deyil.",
            TimeoutException => "Gözləmə vaxtı bitdi.",
            SecurityTokenExpiredException => "Token müddəti bitmişdir.",
            EntityExistException => "Bu məlumat artıq mövcuddur.",
            SecurityException => "İcazə verilmir.",
            _ => "Gözlənilməyən xəta baş verdi. Daha sonra yenidən cəhd edin."
        };

        var errorResponse = new
        {
            status = statusCode,
            message = userMessage,
            detail = exception.Message,
            traceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await response.WriteAsync(json);
    }
}
