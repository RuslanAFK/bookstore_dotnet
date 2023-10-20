using Domain.Models;

namespace Data.Abstractions;

public interface IBooksRepository : IBaseRepository<Book>
{
    Task<Book> GetByIdIncludingBookFilesAsync(int id);
    Task<Book> GetByIdAsync(int id);
}