using Domain.Models;
using Services.ResponseDtos;

namespace Services.Extensions;

public static class FilterExtensions
{
    public static IQueryable<GetBooksDto> ToGetBooksDto(this IQueryable<Book> books)
    {
        return books.Select(x => new GetBooksDto
        {
            Id = x.Id,
            Name = x.Name,
            Image = x.Image
        }); 
    }
    
    public static IQueryable<GetUsersDto> ToGetUsersDto(this IQueryable<User> books)
    {
        return books.Select(x => new GetUsersDto
        {
            Id = x.Id,
            RoleName = x.Role!.RoleName,
            Username = x.Name
        }); 
    }
}