using AutoMapper;
using Shop.Api.ViewModels.User;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.EditAddress;

namespace Shop.Api.Infrastructure;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<AddUserAddressCommand, AddUserAddressViewModel>().ReverseMap();
        CreateMap<EditUserAddressCommand, EditUserAddressViewModel>().ReverseMap();
    }
}