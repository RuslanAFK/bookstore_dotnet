using Domain.Models;

namespace Domain.Abstractions;

public interface IBooksRepository : IBaseRepository<Book>
{
    Task<ListResponse<Book>> GetQueriedItemsAsync(Query query);
    Task<Book> GetIncludingBookFilesAsync(int id);
}