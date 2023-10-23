

using Common.Query;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;

namespace Shop.Query.Users.Address;

public record GetUserAddressGetById(long AddressId) : IQuery<AddressDto>;

public class GetUserAddressGetByIdHandler : IQueryHandler<GetUserAddressGetById, AddressDto>
{
    private readonly DapperContext _dapperContext;
    public GetUserAddressGetByIdHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<AddressDto> Handle(GetUserAddressGetById request, CancellationToken cancellationToken)
    {
        using var context = _dapperContext.CreateConnection();
        var sql = @$"select top 1 * from {_dapperContext.UserAddress} where id=@id";
        var result = await context.QuerySingleOrDefaultAsync<AddressDto>(sql, new { id = request.AddressId });
        return result;
    }
}