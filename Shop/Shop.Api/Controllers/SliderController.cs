using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Api.ViewModels.Slider;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Slider;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.SliderManagement)]
public class SliderController : BaseApiController
{
    private readonly ISliderFacade _sliderFacade;

    public SliderController(ISliderFacade sliderFacade)
    {
        _sliderFacade = sliderFacade;
    }

    [HttpGet("{id}")]

    public async Task<ApiResult<SliderDto>> GetById(long id)
    {
        return QueryResult(await _sliderFacade.GetSliderById(id));
    }

    [HttpGet("GetList")]
    [AllowAnonymous]
    public async Task<ApiResult<List<SliderDto>>> GetByList()
    {
        return QueryResult(await _sliderFacade.GetSliders());
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create([FromForm] CreateSliderViewModel model)
    {
        var result = await _sliderFacade.CreateSlider(new CreateSliderCommand(model.Link,model.ImageFile,model.Title));
        var url = Url.Action("GetBYId", "Slider", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditSliderViewModel model)
    {
        return CommandResult(await _sliderFacade.EditSlider(new EditSliderCommand(model.Id,model.Link,model.ImageFile,model.Title)));
    }

    [HttpDelete]
    public async Task<ApiResult> Delete([FromQuery]long id)
    {
        return CommandResult(await _sliderFacade.DeleteSlider(id));
    }

}