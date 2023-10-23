using AutoMapper;
using Shop.Api.ViewModels.User;
using Shop.Application.Users.AddAddress;

namespace Shop.Api.Infrastructure;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<AddUserAddressCommand, AddUserAddressViewModel>().ReverseMap();
    }
}