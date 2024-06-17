namespace Shop.Presentation.Facade;

class CacheKeys
{
    public static string User(long id) => $"user-{id}";
    public static string Product(string slug) => $"product-{slug}";
    public static string HashToken(string hashToken) => $"token-{hashToken}";
    public static string Categories = "Categories";

}