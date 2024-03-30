using Domain.Models;
using Services.Dtos;

namespace Services.Extensions;

public static class FilterExtensions
{
    public static IQueryable<GetBooksDto> ToGetBooksDto(this IQueryable<Book> books)
    {
        return books.Select(x => new GetBooksDto
        {
            Id = x.Id,
            Name = x.Name,
            Image = x.Image,
            Author = x.Author
        }); 
    }
    
    public static IQueryable<GetSingleBookDto> ToGetSingleBookDto(this IQueryable<Book> books)
    {
        return books.Select(book => new GetSingleBookDto
        {
            Id = book.Id,
            Name = book.Name,
            Info = book.Info,
            Genre = book.Genre,
            Image = book.Image,
            Author = book.Author,
            BookFile = book.BookFile != null ? book.BookFile.Url : null
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
    
    public static IQueryable<AuthResult> ToAuthResult(this IQueryable<User> users)
    {
        return users.Select(x => new AuthResult
        {
            Id = x.Id,
            Username = x.Name,
            Password = x.Password,
            Role = x.Role!.RoleName
        }); 
    }
}