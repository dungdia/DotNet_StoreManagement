using System.Text.Json.Serialization;
using DotNet_StoreManagement.SharedKernel.filters;

namespace DotNet_StoreManagement.SharedKernel.configuration;

public static class ControllerConfig
{
    public static IServiceCollection ControllerConfigExtension(this IServiceCollection services, IConfiguration config)
    {     
        services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
        services.AddControllers(options =>
        {
            // options.Filters.Add<ValidationFilter>();
        }).ConfigureApiBehaviorOptions(options =>
        {
            // options.SuppressModelStateInvalidFilter = true;
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                policy => policy
                    .WithOrigins("http://localhost:5173") 
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        });

        return services;
    }
}