using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.AddToken;

public class AddTokenCommandHandler : IBaseCommandHandler<AddTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public AddTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user is null) { return OperationResult.NotFound(); }

        user.AddToken(request.HashJwtToken, request.HashJwtRefreshToken, request.ExpireDateToken, request.ExpireDateRefreshToken, request.Device);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}