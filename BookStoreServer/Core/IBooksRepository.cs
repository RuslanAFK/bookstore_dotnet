using BookStoreServer.Core.Models;

namespace BookStoreServer.Core;

public interface IBooksRepository
{
    Task<List<Book>> GetBooksAsync(BookQuery query);
    Task<Book?> GetBookByIdAsync(int bookId);
    Task CreateBookAsync(Book bookToCreate);
    void UpdateBook(Book bookToUpdate);
    void DeleteBook(Book book);
}