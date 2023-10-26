using Common.Application;

namespace Shop.Application.Users.AddToken;

public class AddTokenCommand : IBaseCommand
{
    public AddTokenCommand(long userId, string hashJwtToken, string hashJwtRefreshToken, DateTime expireDateToken, DateTime expireDateRefreshToken, string device)
    {
        UserId = userId;
        HashJwtToken = hashJwtToken;
        HashJwtRefreshToken = hashJwtRefreshToken;
        ExpireDateToken = expireDateToken;
        ExpireDateRefreshToken = expireDateRefreshToken;
        Device = device;
    }

    public long UserId { get; private set; }
    public string HashJwtToken { get; private set; }
    public string HashJwtRefreshToken { get; private set; }
    public DateTime ExpireDateToken { get; private set; }
    public DateTime ExpireDateRefreshToken { get; private set; }
    public string Device { get; private set; }
}