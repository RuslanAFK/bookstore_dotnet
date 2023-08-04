using Domain.Abstractions;
using Domain.Models;

namespace Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IFileManager _fileManager;
    private readonly IUnitOfWork _unitOfWork;

    public BooksService(IBooksRepository booksRepository, IUnitOfWork unitOfWork, IFileManager fileManager)
    {
        _booksRepository = booksRepository;
        _unitOfWork = unitOfWork;
        _fileManager = fileManager;
    }
    public async Task<ListResponse<Book>> GetQueriedAsync(Query query)
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
        await _unitOfWork.CompleteOrThrowAsync();
    }

    public async Task UpdateAsync(int bookId, Book bookToUpdate)
    {
        AssignId(bookId, bookToUpdate);
        _booksRepository.Update(bookToUpdate);
        await _unitOfWork.CompleteOrThrowAsync();
    }

    private void AssignId(int bookId, Book bookToUpdate)
    {
        bookToUpdate.Id = bookId;
    }
    public async Task RemoveAsync(Book book)
    {
        DeleteFileIfExists(book.BookFile);
        _booksRepository.Remove(book);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    private void DeleteFileIfExists(BookFile? bookFile)
    {
        if (bookFile != null)
            _fileManager.DeleteFile(bookFile.Url);
    }
}