using Data.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BooksRepository : BaseRepository<Book>, IBooksRepository
{
    public BooksRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<Book> GetByIdIncludingBookFilesAsync(int id)
    {
        var books = GetAll().Include(x => x.BookFile);
        return await books.GetByIdAsync(id);
    }
}