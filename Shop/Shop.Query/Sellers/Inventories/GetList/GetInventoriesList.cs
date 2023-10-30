using Common.Query;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetList;

public record GetInventoriesList(long SellerId) : IQuery<List<InventoryDto>>;