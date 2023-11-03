using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;

public class GetCurrentOrderByUserIdHandler : IQueryHandler<GetCurrentOrderByUserId, OrderDto>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetCurrentOrderByUserIdHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto> Handle(GetCurrentOrderByUserId request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders
            .FirstOrDefaultAsync(f => f.UserId == request.Userid && f.Status==OrderStatus.Pending, cancellationToken);
        if (order == null)
            return null;

        var orderDto = order.Map();
        orderDto.UserFullName = await _shopContext.Users.Where(f => f.Id == orderDto.UserId)
            .Select(s => $"{s.Name} {s.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}