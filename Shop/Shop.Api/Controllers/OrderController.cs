using Common.AspNet;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers;

public class OrderController : BaseApiController
{
   private readonly IOrderFacade _orderFacade;

   public OrderController(IOrderFacade orderFacade)
   {
       _orderFacade = orderFacade;
   }

    [HttpGet]
   public async Task<ApiResult<OrderFilterResult>> GetByFilter([FromQuery]OrderFilterParams filterParams)
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

    [HttpDelete("RemoveItem")]
    public async Task<ApiResult> RemoveItem(RemoveOrderItemCommand command)
    {
        var result = await _orderFacade.RemoveOrderItem(command);
        return CommandResult(result);
    }
}