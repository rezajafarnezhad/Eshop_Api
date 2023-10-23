namespace Shop.Api.Infrastructure;

public static class ShopBootstrapper
{
    public static void Init(this ServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
    }
}