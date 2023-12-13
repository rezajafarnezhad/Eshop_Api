using System.Net;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Api.ViewModels.Category;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CategoryManagement)]
public class CategoryController : BaseApiController
{
    private readonly ICategoryFacade _categoryFacade;
    public CategoryController(ICategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ApiResult<List<CategoryDto>>> GetCategories()
    {
        var result = await _categoryFacade.GetCategories();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ApiResult<CategoryDto>> GetCategoryById(long id)
    {
        var result = await _categoryFacade.GetCategoryById(id);
        return QueryResult(result);
    }
    [HttpGet("getChild/{parentId}")]
    public async Task<ApiResult<List<ChildCategoryDto>>> GetCategoriesByParentId(long parentId)
    {
        var result = await _categoryFacade.GetCategoriesByParentId(parentId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult<long>> CreateCategory(CreateCategoryViewModel command)
    {
        var result = await _categoryFacade.Create(new CreateCategoryCommand(command.Title,command.Slug,command.SeoData));
        var url =Url.Action("GetCategoryById","Category",new{id=result.Data},Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created,url);
    }
    [HttpPost("AddChild")]
    public async Task<ApiResult<long>> CreateCategory(AddChildCategoryCommand command)
    {
        var result = await _categoryFacade.AddChild(command);
        var url =Url.Action("GetCategoryById","Category",new{id=result.Data},Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }
    [HttpPut]
    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _categoryFacade.Edit(command);
        return CommandResult(result);

    }
    [HttpDelete("{categoryId}")]
    public async Task<ApiResult> RemoveCategory(long categoryId)
    {
        var result = await _categoryFacade.Remove(categoryId);
        return CommandResult(result);
    }
}