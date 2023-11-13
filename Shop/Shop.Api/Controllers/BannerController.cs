using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Api.ViewModels.Banner;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.BannerManagement)]
public class BannerController : BaseApiController
{
    private readonly IBannerFacade _bannerFacade;

    public BannerController(IBannerFacade bannerFacade)
    {
        _bannerFacade = bannerFacade;
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<BannerDto>> GetById(long id)
    {
        return QueryResult(await _bannerFacade.GetBannerById(id));
    }

    [HttpGet("GetList")]
    [AllowAnonymous]
    public async Task<ApiResult<List<BannerDto>>> GetByList()
    {
        return QueryResult(await _bannerFacade.GetBanners());
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create([FromForm] CreateBannerViewModel  bannerModel)
    {
        var result = await _bannerFacade.CreateBanner(new CreateBannerCommand(bannerModel.Link,bannerModel.ImageFile,bannerModel.Position));
        var url = Url.Action("GetBYId", "Banner", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditBannerViewModel command)
    {
        return CommandResult(await _bannerFacade.EditBanner(new EditBannerCommand(command.Id,command.Link,command.ImageFile,command.Position)));
    }

    [HttpDelete]
    public async Task<ApiResult> Delete([FromQuery] long id)
    {
        return CommandResult(await _bannerFacade.DeleteBanner(id));
    }
}