﻿using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.Edit;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;

namespace Shop.Api.Controllers;

public class ProductController : BaseApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetByFilter([FromQuery]ProductFilterParams filterParams)
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

    [HttpGet("GetBySlug")]
    public async Task<ApiResult<ProductDto>> GetBySlug(string slug)
    {
        var result = await _productFacade.GetProductBySlug(slug);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create([FromForm]CreateProductCommand command)
    {
        var result = await _productFacade.CreateProduct(command);
        var url = Url.Action("GetById", "Product", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> Edit([FromForm]EditProductCommand command)
    {
        var result = await _productFacade.EditProduct(command);
        return CommandResult(result);
    }

    [HttpPost("AddImage")]
    public async Task<ApiResult> AddImage([FromForm] AddProductImageCommand command)
    {
        var result = await _productFacade.AddImage(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> RemoveImage(RemoveProductImageCommand command)
    {
        var result = await _productFacade.RemoveImage(command);
        return CommandResult(result);
    }
}