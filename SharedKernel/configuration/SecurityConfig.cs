using System.Text;
using DotNet_StoreManagement.SharedKernel.security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DotNet_StoreManagement.SharedKernel.configuration;

public static class SecurityConfig
{
    public static IServiceCollection SecurityConfigExtension(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<JwtFilter>();
        });
        
        // services.AddHttpContextAccessor();
        // -- Config authorization --
        // services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        // services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        
        // -- Config openapi security schema -- 
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
        
        // -- Config auth service -- 
        services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            String secretKey = config["jwt:SecretKey"] ?? "";
            String issuer = config["jwt:Issuer"] ?? "";
            String audience = config["jwt:Audience"] ?? "";
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
        
        services.AddMemoryCache();

        services.AddAuthorization();
        
        return services;
    }
}