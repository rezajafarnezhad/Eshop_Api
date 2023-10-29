
using Common.Application;

namespace Shop.Application.Users.RemoveToken;

public class RemoveTokenCommand : IBaseCommand
{
    public RemoveTokenCommand(long userId, long tokenId)
    {
        UserId = userId;
        TokenId = tokenId;
    }

    public long UserId { get;private set; }
    public long TokenId { get; private set; }
}