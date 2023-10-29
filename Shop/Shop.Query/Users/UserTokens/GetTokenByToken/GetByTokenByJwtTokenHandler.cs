using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetTokenByToken;

public class GetByTokenByJwtTokenHandler : IQueryHandler<GetByTokenByJwtToken, UserTokenDto>
{
    private readonly DapperContext _dapperContext;

    public GetByTokenByJwtTokenHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto> Handle(GetByTokenByJwtToken request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $"select top 1 * from {_dapperContext.UserToken} where HashJwtToken=@hashjwtToken";
        return await connection.QuerySingleOrDefaultAsync<UserTokenDto>(sql, new { hashjwtToken = request.HashToken });
    }
}