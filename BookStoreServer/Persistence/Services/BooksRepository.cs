using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using BookStoreServer.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence.Services
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext _context;
        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ListResponse<Book>> GetBooksAsync(QueryObject queryObject)
        {
            var books = _context.Books.ApplySearching(queryObject);
            var response = new ListResponse<Book>()
            {
                Count = books.Count(),
                Items = await books.ApplyPagination(queryObject, 4).ToListAsync()
            };
            return response;
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
            _context.Books.Remove(book);
        }
    }
}
