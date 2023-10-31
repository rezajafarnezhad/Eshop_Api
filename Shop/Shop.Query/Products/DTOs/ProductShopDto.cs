using Common.Query;
using Common.Query.Filter;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Products.DTOs;

public class ProductShopDto : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public long InventoryId { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
    public string ImageName { get; set; }

    public int TotalPrice
    {
        get
        {
            var discount = Price * DiscountPercentage / 100;
            return Price - discount;
        }
    }
}

public class ProductShopFilterParams : BaseFilterParam
{
    public string? CategorySlug { get; set; } = "";
    public string? Search { get; set; } = "";
    public bool OnlyAvailableProduct { get; set; }=false;
    public bool JustHasDiscount { get; set; } = false;
    public ProductSearchOrderBy SearchOrder { get; set; } = ProductSearchOrderBy.Latest;
}

public enum ProductSearchOrderBy
{
    Latest,
    Expensive,
    Cheapest,
}


public class ProductShopResult : BaseFilter<ProductShopDto, ProductShopFilterParams>
{
    public CategoryDto? CategoryDto { get; set; }
}