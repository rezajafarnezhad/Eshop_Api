using Common.Application;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Query.Users.Address;

namespace Shop.Presentation.Facade.Users.Addresses
{
    public interface IUserAddressFacade
    {
        Task<OperationResult> AddAddress(AddUserAddressCommand command);

        Task<OperationResult> EditAddress(EditUserAddressCommand command);
        Task<OperationResult> DeleteAddress(DeleteUserAddressCommand command);
        Task<AddressDto> GetById(long addressId);
        Task<List<AddressDto>> GetList(long userId);
    }
}