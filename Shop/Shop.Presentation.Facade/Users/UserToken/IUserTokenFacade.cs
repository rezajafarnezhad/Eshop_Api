using Common.Application;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users.UserToken;

public interface IUserTokenFacade
{
    Task<UserTokenDto> GetUserTokenByRefreshToken(string refreshToken);
    Task<OperationResult> RemoveToken(RemoveTokenCommand command);
}