using inventory.domain.Contracts;
using inventory.infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace inventory.infrastructure.Cache;

public static class CacheServiceRegistration
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}
