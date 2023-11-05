using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [PermissionChecker(Permission.UserManagement)]
    [HttpGet]
    public async Task<ApiResult<UserFilterResult>> GetByFilter([FromQuery] UserFilterParams filterParams)
    {
        return QueryResult(await _userFacade.GetUserByFilter(filterParams));
    }

    [HttpGet("{id}")]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult<UserDto>> GetById(long id)
    {
        return QueryResult(await _userFacade.GetUserById(id));
    }

    [HttpGet("CurrentUser")]
    public async Task<ApiResult<UserDto>> GetCurrent()
    {
        var userId = User.GetUserId();
        return QueryResult(await _userFacade.GetUserById(userId));
    }

    [HttpGet("GetByPhoneNumber")]
    [PermissionChecker(Permission.UserManagement)]

    public async Task<ApiResult<UserDto>> GetByPhoneNumber(string phoneNumber)
    {
        return QueryResult(await _userFacade.GetUserByPhoneNumber(phoneNumber));
    }

    [HttpPost]
    [PermissionChecker(Permission.UserManagement)]

    public async Task<ApiResult<long>> Create(CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        var url = Url.Action("GetById", "User", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    [PermissionChecker(Permission.UserManagement)]
    public async Task<ApiResult> Edit([FromForm] EditUserCommand command)
    {
        return CommandResult(await _userFacade.EditUser(command));
    }

    [HttpPut("CurrentUser")]
    public async Task<ApiResult> EditCurrentUser([FromForm] EditCurrentUserViewModel command)
    {
        var editUserCommand = new EditUserCommand
            (User.GetUserId(),command.Avatar,command.Name,command.Family,
                command.PhoneNumber,command.Email,command.Gender);
        return CommandResult(await _userFacade.EditUser(editUserCommand));

    }


    [HttpPut("ChangePassword")]
    public async Task<ApiResult> ChangePassword(ChangePasswordViewModel changePassword)
    {
        return CommandResult(await _userFacade.ChangePassword
        (new ChangePasswordCommand(User.GetUserId(),
            changePassword.Password, changePassword.CurrentPassword)));
    }

}