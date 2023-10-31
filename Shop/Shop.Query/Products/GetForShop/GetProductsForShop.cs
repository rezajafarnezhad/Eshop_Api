using Common.Query;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetForShop;

public class GetProductsForShop : QueryFilter<ProductShopResult, ProductShopFilterParams>
{
    public GetProductsForShop(ProductShopFilterParams filterParams) : base(filterParams)
    {
    }
}