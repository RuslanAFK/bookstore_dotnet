using Domain.Models;

namespace Data.Abstractions;

public interface IBooksRepository : IBaseRepository<Book>
{
    Task<Book> GetIncludingBookFilesAsync(int id);
}