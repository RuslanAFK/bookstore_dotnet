using Microsoft.EntityFrameworkCore;

namespace bookstoreserver.Data
{
    public class BooksRepository
    {
        internal async static Task<List<Book>> GetBooksAsync()
        {
            using(var db = new AppDbContext())
            {
                return await db.Books.ToListAsync();
            }
        }
        internal async static Task<Book> GetBookByIdAsync(int bookId)
        {
            using(var db = new AppDbContext())
            {
                return await db.Books.FirstOrDefaultAsync(book => book.Id == bookId);
            }
        }
        internal async static Task<bool> CreateBookAsync(Book bookToCreate)
        {
            using(var db = new AppDbContext())
            {
                try
                {
                    var bookFound = await db.Books.FirstOrDefaultAsync(book => book.Name == bookToCreate.Name
                        && book.Author == bookToCreate.Author);
                    if (bookFound != null)
                    {
                        return false;
                    }
                    await db.Books.AddAsync(bookToCreate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> UpdateBookAsync(Book bookToUpdate)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var bookFound = await db.Books.FirstOrDefaultAsync(book => book.Name == bookToUpdate.Name
                        && book.Author == bookToUpdate.Author);
                    if (bookFound != null)
                    {
                        return false;
                    }
                    db.Books.Update(bookToUpdate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> DeleteBookAsync(int bookId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    Book bookToDelete = await GetBookByIdAsync(bookId);
                    db.Remove(bookToDelete);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

    }
}
