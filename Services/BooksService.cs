using Domain.Abstractions;
using Domain.Models;

namespace Services;

public class BooksService : BaseService, IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IFileManager _fileManager;

    public BooksService(IBooksRepository booksRepository, IUnitOfWork unitOfWork, IFileManager fileManager) : base(unitOfWork)
    {
        _booksRepository = booksRepository;
        _fileManager = fileManager;
    }
    public async Task<ListResponse<Book>> GetBooksAsync(Query query)
    {
        return await _booksRepository.GetQueriedItemsAsync(query);
    }
    public async Task<Book> GetByIdAsync(int bookId)
    {
        return await _booksRepository.GetIncludingBookFilesAsync(bookId);
    }
    public async Task AddAsync(Book bookToCreate)
    {
        await _booksRepository.AddAsync(bookToCreate);
        await CompleteAndCheckIfCompleted();
    }

    public async Task UpdateAsync(int bookId, Book bookToUpdate)
    {
        bookToUpdate.Id = bookId;
        _booksRepository.Update(bookToUpdate);
        await CompleteAndCheckIfCompleted();
    }
    public async Task RemoveAsync(Book book)
    {
        DeleteFileIfExists(book.BookFile);
        _booksRepository.Remove(book);
        await CompleteAndCheckIfCompleted();
    }
    private void DeleteFileIfExists(BookFile? bookFile)
    {
        if (bookFile != null)
            _fileManager.DeleteFile(bookFile.Url);
    }
}