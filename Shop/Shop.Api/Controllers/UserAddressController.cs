using AutoMapper;
using Common.AspNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.User;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users.Address;

namespace Shop.Api.Controllers;

[Authorize]
public class UserAddressController : BaseApiController
{
    private readonly IUserAddressFacade _userAddressFacade;
    private readonly IMapper _mapper;
    public UserAddressController(IUserAddressFacade userAddressFacade, IMapper mapper)
    {
        _userAddressFacade = userAddressFacade;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ApiResult<List<AddressDto>>> GetList()
    {
        var userId = User.GetUserId();
        return QueryResult(await _userAddressFacade.GetList(userId));
    }

    [HttpGet("GetById")]
    public async Task<ApiResult<AddressDto>> GetById(long addressId)
    {
        return QueryResult(await _userAddressFacade.GetById(addressId));
    }

    [HttpPost]
    public async Task<ApiResult> AddAddress(AddUserAddressViewModel command)
    {
        var mapCommand = _mapper.Map<AddUserAddressCommand>(command);
        mapCommand.UserId = User.GetUserId();
        return CommandResult(await _userAddressFacade.AddAddress(mapCommand));
    }

    [HttpPut]
    public async Task<ApiResult> EditAddress(EditUserAddressViewModel command)
    {
        var mapCommand = _mapper.Map<EditUserAddressCommand>(command);
        mapCommand.UserId = User.GetUserId();
        return CommandResult(await _userAddressFacade.EditAddress(mapCommand));
    }

    [HttpDelete]
    public async Task<ApiResult> DeleteAddress([FromQuery]long addressId)
    {
        var userId = User.GetUserId();
        return CommandResult(await _userAddressFacade.DeleteAddress(new DeleteUserAddressCommand(userId, addressId)));
    }
}