using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetTokenByRefreshToken;

public class GetUserTokenByRefreshTokenHandler : IQueryHandler<GetUserTokenByRefreshToken, UserTokenDto>
{

    private readonly DapperContext _dapperContext;

    public GetUserTokenByRefreshTokenHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto> Handle(GetUserTokenByRefreshToken request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"select top 1 * From {_dapperContext.UserToken} where HashJwtRefreshToken = @RefreshToken";
        return await connection.QuerySingleOrDefaultAsync<UserTokenDto>(sql, new {RefreshToken=request.HashRefreshToken});
    }
}