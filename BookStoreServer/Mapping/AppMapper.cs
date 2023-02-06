using AutoMapper;
using BookStoreServer.Controllers.Resources;
using BookStoreServer.Controllers.Resources.Users;
using BookStoreServer.Core.Models;

namespace BookStoreServer.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginResource, User>();
        CreateMap<RegisterResource, User>();
        CreateMap<CreateBookResource, Book>();
        CreateMap<UpdateBookResource, Book>();

        CreateMap<Book, GetBooksResource>();
        CreateMap<Book, GetSingleBookResource>();
        CreateMap<User, GetUsersResource>()
            .ForMember(resource => resource.RoleName, opt => 
                opt.MapFrom(user => user.Role.RoleName));
        
        CreateMap(typeof(ListResponse<>), typeof(ListResponseResource<>));
    }
}