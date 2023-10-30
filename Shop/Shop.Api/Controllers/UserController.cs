using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers;


[PermissionChecker(Permission.UserManagement)]
public class UserController : BaseApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }
    [HttpGet]
    public async Task<ApiResult<UserFilterResult>> GetByFilter([FromQuery] UserFilterParams filterParams)
    {
        return QueryResult(await _userFacade.GetUserByFilter(filterParams));
    }
    [HttpGet("{id}")]
    public async Task<ApiResult<UserDto>> GetById(long id)
    {
        return QueryResult(await _userFacade.GetUserById(id));
    }

    [HttpGet("GetByPhoneNumber")]
    public async Task<ApiResult<UserDto>> GetByPhoneNumber(string phoneNumber)
    {
        return QueryResult(await _userFacade.GetUserByPhoneNumber(phoneNumber));
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        var url = Url.Action("GetById", "User", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditUserCommand command)
    {
        return CommandResult(await _userFacade.EditUser(command));
    }


}