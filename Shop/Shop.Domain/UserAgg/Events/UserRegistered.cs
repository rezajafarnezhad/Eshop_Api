using Common.Domain;

namespace Shop.Domain.UserAgg.Events;

public class UserRegistered : BaseDomainEvent
{
    public UserRegistered(long userId, string phoneNumber)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
    }

    public long UserId { get;private set; }
    public string PhoneNumber { get;private set; }
}