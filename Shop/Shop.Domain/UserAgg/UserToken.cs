using Common.Domain;
using Common.Domain.Exceptions;

public class UserToken : BaseEntity
{
    private UserToken()
    {
            
    }
    public UserToken(string hashJwtToken, string hashJwtRefreshToken, DateTime expireDateToken, DateTime expireDateRefreshToken, string device)
    {
        Guard(hashJwtToken,hashJwtRefreshToken,expireDateToken,expireDateRefreshToken);
        HashJwtToken = hashJwtToken;
        HashJwtRefreshToken = hashJwtRefreshToken;
        ExpireDateToken = expireDateToken;
        ExpireDateRefreshToken = expireDateRefreshToken;
        Device = device;
    }

    public long UserId { get;  set; }
    public string HashJwtToken { get; private set; }
    public string HashJwtRefreshToken { get; private set; }
    public DateTime ExpireDateToken { get; private set; }
    public DateTime ExpireDateRefreshToken { get; private set; }
    public string Device { get; private set; }


    public void Guard(string hashJwtToken, string hashJwtRefreshToken, DateTime expireDateToken, DateTime expireDateRefreshToken)
    {
        NullOrEmptyDomainDataException.CheckString(hashJwtToken, nameof(hashJwtToken));
        NullOrEmptyDomainDataException.CheckString(hashJwtRefreshToken, nameof(hashJwtRefreshToken));

        if (expireDateToken < DateTime.Now)
            throw new InvalidDataException("ExpireDate Token Invalid");

        if (expireDateRefreshToken < ExpireDateToken)
            throw new InvalidDataException("ExpireDate RefreshToken Invalid");
    }
}