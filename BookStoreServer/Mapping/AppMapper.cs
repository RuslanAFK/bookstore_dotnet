using AutoMapper;
using BookStoreServer.Controllers.Resources.Auth;
using BookStoreServer.Controllers.Resources.Books;
using BookStoreServer.Controllers.Resources.Users;
using BookStoreServer.Core.Models;

namespace BookStoreServer.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginResource, User>()
            .ForMember(user => user.Name, opt => 
                opt.MapFrom(resource => resource.Username));
        CreateMap<RegisterResource, User>()
            .ForMember(user => user.Name, opt => 
                opt.MapFrom(resource => resource.Username));
        CreateMap<CreateBookResource, Book>();
        CreateMap<UpdateBookResource, Book>();
        CreateMap<UpdateUserRoleResource, User>()
            .ForMember(user => user.Name, opt =>
                opt.Ignore())
            .ForMember(user => user.Password, opt =>
                opt.Ignore());
        CreateMap<UpdateUserInfoResource, User>()
            .ForMember(user => user.Name, opt =>
                opt.MapFrom(resource => resource.Username))
            .ForMember(user => user.Password, opt =>
                opt.Ignore());

        CreateMap<Book, GetBooksResource>();
        CreateMap<Book, GetSingleBookResource>();
        CreateMap<User, GetUsersResource>()
            .ForMember(resource => resource.Username, opt => 
                opt.MapFrom(user => user.Name))
            .ForMember(resource => resource.RoleName, opt => 
                opt.MapFrom(user => user.Role.RoleName));
        
        CreateMap(typeof(ListResponse<>), typeof(ListResponseResource<>));
    }
}