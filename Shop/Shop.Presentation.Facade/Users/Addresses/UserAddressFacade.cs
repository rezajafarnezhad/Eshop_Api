using Common.Application;
using MediatR;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Query.Users.Address;

namespace Shop.Presentation.Facade.Users.Addresses
{
    internal class UserAddressFacade : IUserAddressFacade
    {
        private readonly IMediator _mediator;

        public UserAddressFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddAddress(AddUserAddressCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditAddress(EditUserAddressCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteAddress(DeleteUserAddressCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<AddressDto>> GetList(long userId)
        {
            return await _mediator.Send(new GetUserAddressList(userId));
        }

        public async Task<AddressDto> GetById(long addressId)
        {
            return await _mediator.Send(new GetUserAddressGetById(addressId));
        }
    }
}