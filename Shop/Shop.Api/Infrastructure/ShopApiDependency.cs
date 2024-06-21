using AspNetCoreRateLimit;
using Shop.Api.Infrastructure.jwt;

namespace Shop.Api.Infrastructure;

public static class ShopApiDependency
{
    public static void RegisterApiDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddTransient<CustomJwtValid>();

        services.AddMemoryCache();

        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}