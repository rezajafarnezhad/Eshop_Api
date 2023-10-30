using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.RoleManagement)]
public class RoleController : BaseApiController
{
    private readonly IRoleFacade _roleFacade;

    public RoleController(IRoleFacade roleFacade)
    {
        _roleFacade = roleFacade;
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<RoleDto>> GetById(long id)
    {
        return QueryResult(await _roleFacade.GetRoleById(id));
    }

    [HttpGet]
    public async Task<ApiResult<List<RoleDto>>> GetList()
    {
        return QueryResult(await _roleFacade.GetRoles());
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateRoleCommand command)
    {
        var result = await _roleFacade.CreateRole(command);
        var url = Url.Action("GetById", "Role", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditRoleCommand command)
    {
        return CommandResult(await _roleFacade.EditRole(command));
    }
}