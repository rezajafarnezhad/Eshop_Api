using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetById;

public class GetInventoryByIdHandler : IQueryHandler<GetInventoryById, InventoryDto?>
{
    private readonly DapperContext _dapperContext;
    public GetInventoryByIdHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<InventoryDto?> Handle(GetInventoryById request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = @$"SELECT Top(1) i.Id, i.SellerId , i.ProductId ,i.Count,i.CreationDate 
, i.Price ,i.DiscountPercentage , s.ShopName,
                        p.Title as ProductName,p.ImageName as ProductImage
            FROM 
        {_dapperContext.Inventories} i inner join {_dapperContext.Sellers} s on i.SellerId=s.Id  
            inner join {_dapperContext.Products} p on i.ProductId=p.Id WHERE i.Id=@id";

        var inventory = await connection.QuerySingleOrDefaultAsync<InventoryDto>(sql, new { id = request.InventoryId });
        if (inventory is null)
            return null;

        return inventory;
    }
}