using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetById;

public record GetInventoryById(long InventoryId) : IQuery<InventoryDto?>;