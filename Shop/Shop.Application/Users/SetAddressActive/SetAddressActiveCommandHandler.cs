using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetAddressActive;

public class SetAddressActiveCommandHandler : IBaseCommandHandler<SetAddressActiveCommand>
{
    private readonly IUserRepository _userRepository;
    public SetAddressActiveCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetAddressActiveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if(user is null)
            return OperationResult.NotFound("کاربری بافت نشد");

        user.SetAddressActive(request.AddressId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}