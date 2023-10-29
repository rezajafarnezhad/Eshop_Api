using Common.AspNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.UserToken;

namespace Shop.Api.Infrastructure.jwt;

public  class CustomJwtValid
{
    private readonly IUserFacade _userFacade;
    private readonly IUserTokenFacade _userTokenFacade;
    public CustomJwtValid(IUserFacade userFacade, IUserTokenFacade userTokenFacade)
    {
        _userFacade = userFacade;
        _userTokenFacade = userTokenFacade;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        var token = await _userTokenFacade.GetUserTokenByJwtToken(jwtToken);
        if (token == null)
        {
            context.Fail("Token NotFound");
            return;
        }

        var userId = context.Principal.GetUserId();
        var user = await _userFacade.GetUserById(userId);
        if (user ==null || user.IsAcive == false)
        {
            context.Fail("User Not Active");
            return;
        }
    }


}