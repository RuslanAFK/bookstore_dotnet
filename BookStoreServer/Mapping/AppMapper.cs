using AutoMapper;
using BookStoreServer.Controllers.Resources;
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
        CreateMap(typeof(ListResponse<>), typeof(ListResponseResource<>));
    }
}