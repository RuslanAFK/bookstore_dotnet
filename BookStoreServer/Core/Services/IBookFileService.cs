using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IBookFileService
{
    Task AddBookFile(Book book, IFormFile file);
    Task RemoveBookFile(Book book);
}