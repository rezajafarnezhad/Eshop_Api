using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.ChangePassword;

public class ChangePasswordCommandHandler : IBaseCommandHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if(user == null)
            return OperationResult.NotFound();

        if (user.Password != Sha256Hasher.Hash(request.CurrentPass))
        {
            return OperationResult.Error("کلمه عبور معتبر نیست");
        }
        user.ChangePassword(Sha256Hasher.Hash(request.Pass));
        await _userRepository.Save();
        return OperationResult.Success("کلمه عبور با موفقیت تغییر کرد");
    }

  
}