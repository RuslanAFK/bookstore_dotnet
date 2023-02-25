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
        
        CreateMap<UpdateUserInfoResource, User>()
            .ForMember(user => user.Name, opt =>
                opt.MapFrom(resource => resource.Username));
        
        CreateMap<Book, GetBooksResource>();
        CreateMap<Book, GetSingleBookResource>()
            .ForMember(r => r.BookFile, opt =>
                opt.MapFrom(book => book.BookFile != null ? book.BookFile.Url : null));
        CreateMap<User, GetUsersResource>()
            .ForMember(resource => resource.Username, opt => 
                opt.MapFrom(user => user.Name))
            .ForMember(resource => resource.RoleName, opt => 
                opt.MapFrom(user => user.Role.RoleName));
        CreateMap<AuthResult, AuthResultResource>();

        CreateMap(typeof(ListResponse<>), typeof(ListResponseResource<>));
    }
}