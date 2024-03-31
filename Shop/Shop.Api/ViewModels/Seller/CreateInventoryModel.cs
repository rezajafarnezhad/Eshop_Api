namespace Shop.Api.ViewModels.Seller;

public class CreateInventoryModel
{
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? PercentageDiscount { get; set; }
}

public class EditInventoryModel
{
    public long SellerId { get; set; }
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? DiscountPercentage { get; set; }
}
