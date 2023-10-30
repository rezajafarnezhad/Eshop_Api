using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByUserId;

public class GetSellerByUserIdHandler : IQueryHandler<GetSellerByUserId, SellerDto>
{
    private readonly ShopContext ShopContext;

    public GetSellerByUserIdHandler(ShopContext shopContext)
    {
        ShopContext = shopContext;
    }

    public async Task<SellerDto> Handle(GetSellerByUserId request, CancellationToken cancellationToken)
    {
        var seller = await ShopContext.Sellers.SingleOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken: cancellationToken);
        return seller.Map();
    }
}