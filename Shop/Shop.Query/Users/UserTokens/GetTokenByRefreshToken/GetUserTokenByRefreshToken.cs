using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetTokenByRefreshToken;

public class GetUserTokenByRefreshToken : IQuery<UserTokenDto>
{
    public GetUserTokenByRefreshToken(string hashRefreshToken)
    {
        HashRefreshToken = hashRefreshToken;
    }

    public string HashRefreshToken { get;private set; }
}