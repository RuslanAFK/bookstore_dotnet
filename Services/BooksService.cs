using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IHostEnvironment _host;
        public BooksService(IBooksRepository booksRepository, IUnitOfWork unitOfWork, IFileStorage fileStorage,
            IHostEnvironment host)
        {
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
            _host = host;
        }
        public async Task<ListResponse<Book>> GetBooksAsync(QueryObject queryObject)
        {
            return await _booksRepository.GetBooksAsync(queryObject);
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _booksRepository.GetBookByIdAsync(bookId);
        }
        public async Task<bool> CreateBookAsync(Book bookToCreate)
        {
            await _booksRepository.CreateBookAsync(bookToCreate);
            return await IsCompleted();
        }

        public async Task<bool> UpdateBookAsync(int bookId, Book bookToUpdate)
        {
            bookToUpdate.Id = bookId;
            _booksRepository.UpdateBook(bookToUpdate);
            return await IsCompleted();
        }
        public async Task<bool> DeleteBookAsync(Book book)
        {
            RemoveFileFromBook(book);
            _booksRepository.DeleteBook(book);
            return await IsCompleted();
        }

        public async Task<bool> AddFileToBookAsync(Book book, IFormFile file)
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
            return await IsCompleted();
        }

        public async Task<bool> RemoveFileFromBookAsync(Book book)
        {
            if (book.BookFile == null)
                return false;
            var removed = RemoveFileFromBook(book);
            return await IsCompleted() && removed;
        }

        private async Task<bool> IsCompleted()
        {
            return await _unitOfWork.CompleteAsync() > 0;
        }

        private bool RemoveFileFromBook(Book book)
        {
            var uploadsPath = Path.Combine(_host.ContentRootPath, "wwwroot", "uploads");

            if (book.BookFile == null)
                return false;
            
            // Delete from wwwroot
            _fileStorage.DeleteFile(uploadsPath, book.BookFile.Url);
                
            // Delete from db
            _booksRepository.DeleteFileFromBook(book.BookFile);

            return true;
        }
    }
}