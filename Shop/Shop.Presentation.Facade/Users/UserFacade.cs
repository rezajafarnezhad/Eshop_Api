using Common.Application;
using Common.AspNet;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;

namespace Shop.Presentation.Facade.Users;
internal class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _distributedCache;
    public UserFacade(IMediator mediator, IDistributedCache distributedCache)
    {
        _mediator = mediator;
        _distributedCache = distributedCache;
    }
    public async Task<OperationResult<long>> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(EditUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
            await _distributedCache.RemoveAsync(CacheKeys.User(command.UserId));

        return result;
    }

    public async Task<OperationResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
            await _distributedCache.RemoveAsync(CacheKeys.User(command.UserId));

        return result;
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        return await _distributedCache.GetOrSet(CacheKeys.User(userId), () =>
        {
            return _mediator.Send(new GetUserByIdQuery(userId));
        });
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }
    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToken(AddTokenCommand command)
    {
        return await _mediator.Send(command);
    }
}