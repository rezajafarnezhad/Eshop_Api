using Common.Application;

namespace Shop.Application.Users.SetAddressActive;

public record SetAddressActiveCommand(long UserId,long AddressId) : IBaseCommand;