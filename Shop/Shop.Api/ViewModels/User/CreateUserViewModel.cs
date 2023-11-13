using Shop.Domain.UserAgg.Enums;

namespace Shop.Api.ViewModels.User;

public class CreateUserViewModel
{
    public string Name { get;  set; }
    public string Family { get;  set; }
    public string PhoneNumber { get;  set; }
    public string Email { get;  set; }
    public string Password { get;  set; }
    public Gender Gender { get;  set; }
}

public class EditUserViewModel
{
    public long UserId { get;  set; }
    public IFormFile? Avatar { get;  set; }
    public string Name { get;  set; }
    public string Family { get;  set; }
    public string PhoneNumber { get;  set; }
    public string Email { get;  set; }
    public Gender Gender { get;  set; }
}