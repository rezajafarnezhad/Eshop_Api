using AutoMapper;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Api.ViewModels.Product;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;
using System.Net;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.ProductManagement)]
public class ProductController : BaseApiController
{
    private readonly IProductFacade _productFacade;
    private readonly IMapper _mapper;
    public ProductController(IProductFacade productFacade, IMapper mapper)
    {
        _productFacade = productFacade;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ApiResult<ProductFilterResult>> GetByFilter([FromQuery] ProductFilterParams filterParams)
    {
        var result = await _productFacade.GetProductsByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<ProductDto>> GetById(long id)
    {
        var result = await _productFacade.GetProductById(id);
        return QueryResult(result);
    }

    [HttpGet("GetBySlug/{slug}")]
    [AllowAnonymous]
    public async Task<ApiResult<ProductDto>> GetBySlug(string slug)
    {
        var result = await _productFacade.GetProductBySlug(slug);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create([FromForm] ProductViewModel model)
    {
        var result = await _productFacade.CreateProduct(new CreateProductCommand(model.Title, model.ImageFile, model.Description,
            model.CategoryId, model.SubCategoryId, model.SecondarySubCategoryId, model.Slug, model.SeoData.map(), model.MapSpecifications()));
        var url = Url.Action("GetById", "Product", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditProductViewModel model)
    {
        var result = await _productFacade.EditProduct(new EditProductCommand(model.ProductId, model.Title, model.ImageFile, model.Description,
            model.CategoryId, model.SubCategoryId, model.SecondarySubCategoryId, model.Slug, model.SeoData.map(), model.MapSpecifications()));
        return CommandResult(result);
    }

    [HttpPost("AddImage")]
    public async Task<ApiResult> AddImage([FromForm] AddProductImageCommand command)
    {
        var result = await _productFacade.AddImage(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveImage/{productId}/{imageId}")]
    public async Task<ApiResult> RemoveImage(long productId, long imageId)
    {
        var result = await _productFacade.RemoveImage(new RemoveProductImageCommand(productId, imageId));
        return CommandResult(result);
    }

    [HttpGet("ProductShop")]
    [AllowAnonymous]
    public async Task<ApiResult<ProductShopResult>> GetProductShop(
        [FromQuery] ProductShopFilterParams parasFilterParams)
    {
        return QueryResult(await _productFacade.GetProductShop(parasFilterParams));
    }


}