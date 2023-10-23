
using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;

namespace Shop.Query.Users.Address;

public record GetUserAddressList(long UserId) : IQuery<List<AddressDto>>;

public class GetUserAddressListHandler : IQueryHandler<GetUserAddressList, List<AddressDto>>
{
    private readonly DapperContext _dapperContext;
    public GetUserAddressListHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<AddressDto>> Handle(GetUserAddressList request, CancellationToken cancellationToken)
    {
        using var context = _dapperContext.CreateConnection();
        var sql = @$"select * from {_dapperContext.UserAddress} where UserId=@userid";
        var result = await context.QueryAsync<AddressDto>(sql, new { userid = request.UserId });
        return result.ToList();
    }
}