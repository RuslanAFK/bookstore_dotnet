using BookStoreServer.Core.Models;

namespace BookStoreServer.Core;

public interface IBooksRepository
{
    Task<List<Book>> GetBooksAsync();
    Task<Book?> GetBookByIdAsync(int bookId);
    Task CreateBookAsync(Book bookToCreate);
    void UpdateBook(Book bookToUpdate);
    void DeleteBook(Book book);
}