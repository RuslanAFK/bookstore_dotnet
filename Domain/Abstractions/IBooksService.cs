using Domain.Models;

namespace Domain.Abstractions;

public interface IBooksService
{
    Task<ListResponse<Book>> GetBooksAsync(Query query);
    Task<Book> GetByIdAsync(int bookId);
    Task AddAsync(Book bookToCreate);
    Task UpdateAsync(int bookId, Book bookToUpdate);
    Task RemoveAsync(Book book);
}