using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Users;

namespace Shop.Api.Infrastructure.AuthorizeAttr;

public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly Permission permission;
    private IUserFacade _userFacade;
    private IRoleFacade _roleFacade;
    public PermissionChecker(Permission permission)
    {
        this.permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context))
            return;

        _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
        _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("unAuthorize");
        }
    }

    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var f in metaData)
        {
            try
            {
                hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch
            {
                // ignored
            }
        }

        return hasAllowAnonymous;
    }

    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.User.GetUserId();
        var user = await _userFacade.GetUserById(userId);
        if (user == null)
        {
            return false;
        }

        var roleIds = user.Roles.Select(c => c.RoleId).ToList();
        var roles = await _roleFacade.GetRoles();
        var userRoles = roles.Where(c => roleIds.Contains(c.Id));
        return userRoles.Any(c => c.Permissions.Contains(permission));
    }
}