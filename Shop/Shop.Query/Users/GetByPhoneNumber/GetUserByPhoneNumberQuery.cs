using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQuery : IQuery<UserDto?>
{
    public GetUserByPhoneNumberQuery(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get;private set; }
} 