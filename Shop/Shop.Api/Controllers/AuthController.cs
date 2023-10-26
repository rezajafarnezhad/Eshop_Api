using System.Security.Cryptography;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNet;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.jwt;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;

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
    public async Task<ApiResult<string>> Login(LoginViewModel login)
    {

        var user =await _userFacade.GetUserByPhoneNumber(login.PhoneNumber);
        if (user == null)
            return CommandResult(OperationResult<string>.Error("کاربری یافت نشد"));

        if(!Sha256Hasher.IsCompare(user.Password,login.Password))
            return CommandResult(OperationResult<string>.Error("کاربری یافت نشد"));

        if (!user.IsAcive)
            return CommandResult(OperationResult<string>.Error("حساب کاربری غیر فعال است"));

        var token = GenerateJwtToke.GenerateToke(user, _configuration);

        return new ApiResult<string>()
        {
            IsSuccess = true,
            Data = token,
            MetaData = new MetaData()
        };
    }

    [HttpPost("/Register")]
    public async Task<ApiResult> Register(RegisterViewModel register)
    {
        return CommandResult(await _userFacade.RegisterUser(
            new RegisterUserCommand(new PhoneNumber(register.PhoneNumber),
                register.Password)));
    }

}