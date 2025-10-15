using System.Security.Claims;
using System.Text.RegularExpressions;
using DotNet_StoreManagement.SharedKernel.exception;
using DynamicExpresso;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNet_StoreManagement.SharedKernel.security;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PreAuthorizeAttribute : System.Attribute, IAsyncAuthorizationFilter
{
    private readonly String _expression;
    private static readonly Interpreter _interpreter = new();
    private static readonly Dictionary<String, Lambda> _cache = new();

    public PreAuthorizeAttribute(String expression)
    {
        _expression = expression;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext ctx)
    {
        var user = ctx.HttpContext.User;

        var variables = new Dictionary<String, Object?>();

        foreach (var kv in ctx.RouteData.Values)
        {
            variables[kv.Key] = kv.Value?.ToString();
        }
        
        foreach (var kv in ctx.HttpContext.Request.Query)
        {
            variables[kv.Key] = kv.Value.ToString();
        }

        variables["principal"] = new PrincipalWrapper(user);

        _interpreter.SetFunction("hasRole", (Func<String, Boolean>)(role => user.IsInRole(role)));
        _interpreter.SetFunction("hasAuthority", (Func<string, bool>)(auth =>
            user.Claims.Any(c => c.Type == "permission" && c.Value == auth))
        );

        // var parsedExpression = Regex.Replace(
        //     _expression,
        //     @"#(\w+)",
        //     m => m.Groups[1].Value
        // );

        // Convert single quotes to double quotes for string literals
        var parsedExpression = Regex.Replace(
            _expression,
            @"'([^']*)'",
            "\"$1\""
        );

        if (!_cache.TryGetValue(parsedExpression, out var lambda))
        {
            var parameters = variables.Select(v => 
                new Parameter(v.Key, v.Value?.GetType() ?? typeof(String))
            ).ToArray();

            lambda = _interpreter.Parse(parsedExpression, typeof(Boolean), parameters);
            _cache[parsedExpression] = lambda;
        }
        
        bool allowed = (bool)lambda.Invoke(variables.Values.ToArray());

        if (!allowed)
        {
            throw APIException.Forbidden("You don't have permission to access this resource");
        }

        return Task.CompletedTask;
    }

    public class PrincipalWrapper
    {
        public string? Id { get; set; }
        public ClaimsPrincipal User { get; set; }

        public PrincipalWrapper(ClaimsPrincipal user)
        {
            User = user;
            Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}