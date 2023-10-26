using System.Security.Cryptography;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNet;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.jwt;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using UAParser;

namespace Shop.Api.Controllers;

public class AuthController : BaseApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;
    public AuthController(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
    }

    [HttpPost("/Login")]
    public async Task<ApiResult<LoginResultDto>> Login(LoginViewModel login)
    {

        var user = await _userFacade.GetUserByPhoneNumber(login.PhoneNumber);
        if (user == null)
            return CommandResult(OperationResult<LoginResultDto>.Error("کاربری یافت نشد"));

        if (!Sha256Hasher.IsCompare(user.Password, login.Password))
            return CommandResult(OperationResult<LoginResultDto>.Error("کاربری یافت نشد"));

        if (!user.IsAcive)
            return CommandResult(OperationResult<LoginResultDto>.Error("حساب کاربری غیر فعال است"));

        var result =await AddTokenAndGenerateJWt(user);
        return CommandResult(result);
    }

    private async Task<OperationResult<LoginResultDto>> AddTokenAndGenerateJWt(UserDto user)
    {
        #region GetDeviceInfo

        var uaParser = Parser.GetDefault();
        var header = HttpContext.Request.Headers["user-agent"].ToString();
        var device = "windows";
        if (header != null)
        {
            var info = uaParser.Parse(header);
            device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        }

        #endregion


        #region GeneratToken

        var token = GenerateJwtToke.GenerateToke(user, _configuration);

        var refreshToken = Guid.NewGuid().ToString();

        #endregion


        #region HashTokens

        var hashJwtToken = Sha256Hasher.Hash(token);
        var hashJwtRefreshToken = Sha256Hasher.Hash(refreshToken);


        #endregion

        #region AddTokensInUserTokenDatabase

        var resultAddToken = await _userFacade.AddToken(new AddTokenCommand(user.Id, hashJwtToken, hashJwtRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

        if (resultAddToken.Status != OperationResultStatus.Success)
        {
            return OperationResult<LoginResultDto>.Error();
        }
        #endregion


        return OperationResult<LoginResultDto>.Success(new LoginResultDto()
        {
            Token = token,
            RefreshToken = refreshToken,
        });
    }

    [HttpPost("/Register")]
    public async Task<ApiResult> Register(RegisterViewModel register)
    {
        return CommandResult(await _userFacade.RegisterUser(
            new RegisterUserCommand(new PhoneNumber(register.PhoneNumber),
                register.Password)));
    }

}