using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetTokenByToken;

public class GetByTokenByJwtToken : IQuery<UserTokenDto>
{
    public GetByTokenByJwtToken(string hashToken)
    {
        HashToken = hashToken;
    }
    public string HashToken { get;private set; }
}