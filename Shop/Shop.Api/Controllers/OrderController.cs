using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IOrderFacade _orderFacade;

    public OrderController(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    [HttpGet]
    [PermissionChecker(Permission.OrderManagement)]
    public async Task<ApiResult<OrderFilterResult>> GetByFilter([FromQuery] OrderFilterParams filterParams)
    {
        var result = await _orderFacade.GetOrdersByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<OrderDto>> GetById(long id)
    {
        var result = await _orderFacade.GetOrderById(id);
        return QueryResult(result);
    }

    [HttpGet("Current")]
    public async Task<ApiResult<OrderDto>> GetCurrentOrder()
    {
        var result = await _orderFacade.GetCurrentOrder(User.GetUserId());
        return QueryResult(result);
    }

    [HttpPost("AddItem")]
    public async Task<ApiResult> AddItem(AddOrderItemCommand command)
    {
        var result = await _orderFacade.AddOrderItem(command);
        return CommandResult(result);
    }

    [HttpPost("ChackOut")]
    public async Task<ApiResult> ChackOut(CheckoutOrderCommand command)
    {
        var result = await _orderFacade.OrderCheckOut(command);
        return CommandResult(result);
    }

    [HttpPut("DecreaseItemCount")]
    public async Task<ApiResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
    {
        var result = await _orderFacade.DecreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpPut("IncreaseItemCount")]
    public async Task<ApiResult> IncreaseItemCount(IncreaseOrderItemCountCommand command)
    {
        var result = await _orderFacade.IncreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveItem/{itemId}")]
    public async Task<ApiResult> RemoveItem(long itemId)
    {
        var result = await _orderFacade.RemoveOrderItem(new RemoveOrderItemCommand(User.GetUserId(), itemId));
        return CommandResult(result);
    }
}