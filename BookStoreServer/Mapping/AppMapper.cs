using AutoMapper;
using BookStoreServer.Resources.Auth;
using BookStoreServer.Resources.Books;
using BookStoreServer.Resources.Users;
using Domain.Models;

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
        
        CreateMap<UpdateUserInfoResource, User>()
            .ForMember(user => user.Name, opt =>
                opt.MapFrom(resource => resource.Username));
        
        CreateMap<Book, GetSingleBookResource>()
            .ForMember(r => r.BookFile, opt =>
                opt.MapFrom(book => book.BookFile != null ? book.BookFile.Url : null));
       
        CreateMap<AuthResult, AuthResultResource>();
    }
}