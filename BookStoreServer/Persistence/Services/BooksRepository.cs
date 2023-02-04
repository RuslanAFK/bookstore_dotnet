using BookStoreServer.Core;
using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence.Services
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext _context;
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 4;
        private const int MaxPage = 1000;
        private const int MaxPageSize = 20;
        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBooksAsync(BookQuery query)
        {
            var page = query.Page ?? DefaultPage;
            var pageSize = query.PageSize ?? DefaultPageSize;
            page = page is > 0 and < MaxPage ? page : DefaultPage;
            pageSize = pageSize is > 0 and  < MaxPageSize ? pageSize : DefaultPageSize;
            return await _context.Books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }
        public async Task CreateBookAsync(Book bookToCreate)
        {
            await _context.Books.AddAsync(bookToCreate);
        }
        public void UpdateBook(Book bookToUpdate)
        {
            _context.Books.Update(bookToUpdate);
        }
        public void DeleteBook(Book book)
        {
            _context.Remove(book);
        }
    }
}
