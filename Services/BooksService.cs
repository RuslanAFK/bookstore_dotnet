using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;

namespace Services;

public class BooksService : BaseService, IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IFileStorageService _fileStorageService;

    public BooksService(IBooksRepository booksRepository, IUnitOfWork unitOfWork, IFileStorageService fileStorageService) : base(unitOfWork)
    {
        _booksRepository = booksRepository;
        _fileStorageService = fileStorageService;
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
            _fileStorageService.DeleteFile(bookFile.Url);
    }
}