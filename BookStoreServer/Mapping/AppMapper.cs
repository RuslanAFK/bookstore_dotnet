using AutoMapper;
using BookStoreServer.Controllers.Resources;
using BookStoreServer.Core.Models;

namespace BookStoreServer.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserResource>();
        
        CreateMap<LoginResource, User>();
        CreateMap<SignupResource, User>();
    }
}