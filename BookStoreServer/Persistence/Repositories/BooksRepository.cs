using BookStoreServer.Core;
using BookStoreServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Persistence.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext _context;

        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
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
