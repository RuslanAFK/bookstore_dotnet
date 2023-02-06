using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IBooksRepository
{
    Task<ListResponse<Book>> GetBooksAsync(QueryObject queryObject);
    Task<Book?> GetBookByIdAsync(int bookId);
    Task CreateBookAsync(Book bookToCreate);
    void UpdateBook(Book bookToUpdate);
    void DeleteBook(Book book);
}