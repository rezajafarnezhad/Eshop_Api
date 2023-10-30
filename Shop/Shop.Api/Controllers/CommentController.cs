using System.Net;
using Common.Application;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.AuthorizeAttr;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;

namespace Shop.Api.Controllers;

public class CommentController : BaseApiController
{
    private readonly ICommentFacade _commentFacade;
    public CommentController(ICommentFacade commentFacade)
    {
        _commentFacade = commentFacade;
    }

    [HttpGet]
    [PermissionChecker(Permission.CommentManagement)]

    public async Task<ApiResult<CommentFilterResult>> GetByFilter([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _commentFacade.GetCommentsByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    [PermissionChecker(Permission.CommentManagement)]
    public async Task<ApiResult<CommentDto>> GetById(long id)
    {
        var result = await _commentFacade.GetCommentById(id);
        return QueryResult(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ApiResult<long>> Create(CreateCommentCommand command)
    {
        var result = await _commentFacade.CreateComment(command);
        var url = Url.Action("GetById", "Comment", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    [Authorize]
    public async Task<ApiResult> Edit(EditCommentCommand command)
    {
        var result = await _commentFacade.EditComment(command);
        return CommandResult(result);
    }

    [HttpPut("ChangeStatus")]
    [PermissionChecker(Permission.CommentManagement)]
    public async Task<ApiResult> ChangeStatus(ChangeCommentStatusCommand command)
    {
        var result = await _commentFacade.ChangeStatus(command);
        return CommandResult(result);
    }

}