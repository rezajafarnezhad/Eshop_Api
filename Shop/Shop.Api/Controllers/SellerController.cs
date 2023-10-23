using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.Edit;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
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
   public async Task<ApiResult<SellerFilterResult>> GetByFilter([FromQuery]SellerFilterParams filterParams)
   {
       return QueryResult(await _sellerFacade.GetSellersByFilter(filterParams));
   }
   [HttpGet("{id}")]
   public async Task<ApiResult<SellerDto>> GetById(long id)
   {
       return QueryResult(await _sellerFacade.GetSellerById(id));
   }

    [HttpPost]
   public async Task<ApiResult<long>> Create(CreateSellerCommand command)
   {
       var result = await _sellerFacade.CreateSeller(command);
       var url = Url.Action("GetById", "Seller", new { id = result.Data }, Request.Scheme);
       return CommandResult(result, HttpStatusCode.Created, url);
   }
   [HttpPut]
   public async Task<ApiResult> Edit(EditSellerCommand command)
   {
       var result = await _sellerFacade.EditSeller(command);
       return CommandResult(result);
   }

   [HttpPost("AddInventory")]
   public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
   {
       var result = await _sellerInventoryFacade.AddInventory(command);
       return CommandResult(result);
   }

   [HttpPut("EditInventory")]
   public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
   {
       var result = await _sellerInventoryFacade.EditInventory(command);
       return CommandResult(result);
   }
}