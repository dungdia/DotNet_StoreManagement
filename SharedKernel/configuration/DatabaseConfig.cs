using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.SharedKernel.configuration;

public static class DatabaseConfig
{
    public static IServiceCollection DatabaseConfigExtension(this IServiceCollection services, IConfiguration config)
    {
        var REMOTE_MYSQL_URL = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<BaseContext>(options => options.UseMySql(
            REMOTE_MYSQL_URL,
            ServerVersion.AutoDetect(REMOTE_MYSQL_URL)
        ));
        services.AddDbContext<AppDbContext>(options => options.UseMySql(
            REMOTE_MYSQL_URL,
            ServerVersion.AutoDetect(REMOTE_MYSQL_URL)
        ));

        return services;
    }
}