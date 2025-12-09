using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNet_StoreManagement.SharedKernel.security;

[Service]
public class JwtFilter : IAsyncAuthorizationFilter
{
    private readonly JwtTokenProvider _provider;
    private readonly ILogger<JwtFilter> _logger;
    
    public JwtFilter(
        JwtTokenProvider provider,
        ILogger<JwtFilter> logger
    )
    {
        _provider = provider;
        _logger = logger;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext ctx)
    {
        var path = ctx.HttpContext.Request.Path;
        
        if (!path.StartsWithSegments("/api/v1/auth", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }
        
        var request = ctx.HttpContext.Request;
        var authHeader = request.Headers["Authorization"].FirstOrDefault();
        
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            _logger.LogWarning("Unauthorized request: Missing or invalid Authorization header. Path={Path}", request.Path);
            
            throw new APIException(StatusCodes.Status401Unauthorized, $"Unauthorized request: Missing or invalid Authorization header. Path={request.Path}");
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();
        var principal = _provider.validateToken(token);

        if (principal == null)
        {
            throw new APIException(StatusCodes.Status401Unauthorized, "Token is expired");
        }

        ctx.HttpContext.User = principal;

        return Task.CompletedTask;
    }
}