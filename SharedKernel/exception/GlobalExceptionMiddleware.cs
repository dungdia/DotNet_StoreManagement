using DotNet_StoreManagement.Domain.entities.@base;

namespace DotNet_StoreManagement.SharedKernel.exception;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (APIException ex)
        {
            _logger.LogError(ex.Message, "API Exception");
            await HandleExceptionAsync(ctx, ex.statusCode, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Other Exception");
            await HandleExceptionAsync(ctx, StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext ctx, 
        Int32 statusCode,
        Object message
    )
    {
        ctx.Response.StatusCode = statusCode;
        ctx.Response.ContentType = "application/json";

        var errRes = new ErrorResponse(
            statusCode: statusCode,
            message: message,
            timeStamp: DateTime.UtcNow
        );
        
        await ctx.Response.WriteAsJsonAsync(errRes);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}