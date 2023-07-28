using Domain.Abstractions;
using Domain.Models;

namespace Data.Repositories;

public class BooksRepository : SearchableRepository<Book>, IBooksRepository
{
    public BooksRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<ListResponse<Book>> GetQueriedItemsAsync(Query query)
    {
        var books = GetAll();
        return await GetQueriedItemsAsync(query, books);
    }

    public async Task<Book> GetIncludingBookFilesAsync(int id)
    {
        var books = GetAll();
        var booksIncludingBookFiles = GetItemsIncluding(books, book => book.BookFile);
        return await GetByIdAsync(id, booksIncludingBookFiles);
    }
}