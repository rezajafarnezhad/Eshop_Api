using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Application.Comments.Edit;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Sellers.DTOs;

namespace Shop.Api.Controllers;

public class SellerController : BaseApiController
{
    private readonly ISellerFacade _sellerFacade;
    private readonly ISellerInventoryFacade _sellerInventoryFacade;
    public SellerController(ISellerFacade sellerFacade, ISellerInventoryFacade sellerInventoryFacade)
    {
        _sellerFacade = sellerFacade;
        _sellerInventoryFacade = sellerInventoryFacade;
    }

    [HttpGet]
    [PermissionChecker(Permission.SellerManagement)]
    public async Task<ApiResult<SellerFilterResult>> GetByFilter([FromQuery] SellerFilterParams filterParams)
    {
        return QueryResult(await _sellerFacade.GetSellersByFilter(filterParams));
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<SellerDto>> GetById(long id)
    {
        return QueryResult(await _sellerFacade.GetSellerById(id));
    }
    [HttpGet("Current")]
    [Authorize]
    public async Task<ApiResult<SellerDto>> GetByUserId()
    {
        var userId = User.GetUserId();
        return QueryResult(await _sellerFacade.GetSellersByUserId(userId));
    }

    [HttpPost]
    [PermissionChecker(Permission.SellerManagement)]
    public async Task<ApiResult<long>> Create(CreateSellerCommand command)
    {
        var result = await _sellerFacade.CreateSeller(command);
        var url = Url.Action("GetById", "Seller", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }
    [HttpPut]
    [PermissionChecker(Permission.SellerManagement)]
    public async Task<ApiResult> Edit(EditSellerCommand command)
    {
        var result = await _sellerFacade.EditSeller(command);
        return CommandResult(result);
    }

    [HttpPost("AddInventory")]
    [PermissionChecker(Permission.AddInventory)]
    public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.AddInventory(command);
        return CommandResult(result);
    }

    [HttpPut("EditInventory")]
    [PermissionChecker(Permission.EditInventory)]
    public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.EditInventory(command);
        return CommandResult(result);
    }

    [HttpGet("Inventories")]
    [PermissionChecker(Permission.EditInventory)]
    public async Task<ApiResult<List<InventoryDto>>> GetInventories()
    {
        var seller =await _sellerFacade.GetSellersByUserId(User.GetUserId());
        if (seller is null)
            return QueryResult(new List<InventoryDto>());

        var result = await _sellerInventoryFacade.GetListInventories(seller.Id);
        return QueryResult(result);
    }

    [HttpGet("Inventory")]
    [PermissionChecker(Permission.EditInventory)]
    public async Task<ApiResult<InventoryDto>> GetInventoryById(long inventoryId)
    {
        var seller =await _sellerFacade.GetSellersByUserId(User.GetUserId());
        if (seller is null)
            return QueryResult(new InventoryDto());

        var result = await _sellerInventoryFacade.GetInventoryById(inventoryId);
        if (result is null || result.SellerId != seller.Id)
            return QueryResult(new InventoryDto());

        return QueryResult(result);
    }
}