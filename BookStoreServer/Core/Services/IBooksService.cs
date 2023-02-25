using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IBooksService
{
    Task<ListResponse<Book>> GetBooksAsync(QueryObject queryObject);
    Task<Book?> GetBookByIdAsync(int bookId);
    Task<bool> CreateBookAsync(Book bookToCreate);
    Task<bool> UpdateBookAsync(Book bookToUpdate);
    Task<bool> DeleteBookAsync(Book book);
    
    Task<bool> AddFileToBookAsync(Book book, IFormFile file);
    Task<bool> RemoveFileFromBookAsync(Book book);
}