using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.RemoveToken;

public class RemoveTokenHandler : IBaseCommandHandler<RemoveTokenCommand>
{
    private readonly IUserRepository _userRepository;
    public RemoveTokenHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        
        if (user is null)
        { return OperationResult.NotFound(); }

        user.RemoveToken(request.TokenId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}