using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.UserTokens.GetTokenByRefreshToken;

namespace Shop.Presentation.Facade.Users.UserToken;

public class UserTokenFacade : IUserTokenFacade
{
    private readonly IMediator _mediator;
    public UserTokenFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<UserTokenDto> GetUserTokenByRefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshToken(hashRefreshToken));
    }

    public async Task<OperationResult> RemoveToken(RemoveTokenCommand command)
    {
        return await _mediator.Send(command);
    }

}