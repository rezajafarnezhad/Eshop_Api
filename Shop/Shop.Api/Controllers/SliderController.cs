using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
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
    public async Task<ApiResult<long>> Create([FromForm] CreateSliderCommand command)
    {
        var result = await _sliderFacade.CreateSlider(command);
        var url = Url.Action("GetBYId", "Slider", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditSliderCommand command)
    {
        return CommandResult(await _sliderFacade.EditSlider(command));
    }
}