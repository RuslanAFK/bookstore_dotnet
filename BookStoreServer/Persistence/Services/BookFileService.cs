using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;

namespace BookStoreServer.Persistence.Services;

public class BookFileService : IBookFileService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IHostEnvironment _host;

    public BookFileService(IBooksRepository booksRepository, IFileStorage fileStorage, IHostEnvironment host)
    {
        _booksRepository = booksRepository;
        _fileStorage = fileStorage;
        _host = host;
    }
    public async Task AddBookFile(Book book, IFormFile file)
    {
        // Store to wwwroot
        var uploadsPath = Path.Combine(_host.ContentRootPath, "wwwroot", "uploads");
        var relativePath = await _fileStorage.StoreFile(uploadsPath, file);
        
        // Delete if already exists
        if (book.BookFile != null)
            _fileStorage.DeleteFile(uploadsPath, book.BookFile.Url);
        
        // Store in db
        var bookFile = new BookFile { Book = book, Url = relativePath };
        await _booksRepository.AddFileToBook(bookFile);
    }
    public async Task RemoveBookFile(Book book)
    {
        var uploadsPath = Path.Combine(_host.ContentRootPath, "wwwroot", "uploads");
        
        // Delete from wwwroot
        if (book.BookFile != null)
            _fileStorage.DeleteFile(uploadsPath, book.BookFile.Url);
        
        // Delete from db
        _booksRepository.DeleteFileFromBook(book.BookFile);
    }
}