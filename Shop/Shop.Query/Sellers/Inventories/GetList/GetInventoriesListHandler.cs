using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetList;

public class GetInventoriesListHandler : IQueryHandler<GetInventoriesList, List<InventoryDto>>
{
    private readonly DapperContext _dapperContext;
    public GetInventoriesListHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<List<InventoryDto>> Handle(GetInventoriesList request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = @$"select i.Id,i.SellerId,i.ProductId,i.Count,i.Price,i.DiscountPercentage , s.ShopName,
                        p.Title as ProductName,p.ImageName as ProductImage
            FROM 
        {_dapperContext.Inventories} i inner join {_dapperContext.Sellers} s on i.SellerId=s.Id  
            inner join {_dapperContext.Products} p on i.ProductId=p.Id WHERE i.SellerId=@sellerId";

        var inventories = await connection.QueryAsync<InventoryDto>(sql, new { sellerId = request.SellerId });
        return inventories.ToList();
    }
}