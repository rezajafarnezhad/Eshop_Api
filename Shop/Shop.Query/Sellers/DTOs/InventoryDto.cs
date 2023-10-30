using Common.Query;

namespace Shop.Query.Sellers.DTOs;

public class InventoryDto : BaseDto
{
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public string ShopName { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
}