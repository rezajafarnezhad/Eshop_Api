using Shop.Api.Infrastructure.jwt;

namespace Shop.Api.Infrastructure;

public static class ShopApiDependency
{
    public static void RegisterApiDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddTransient<CustomJwtValid>();
    }
}