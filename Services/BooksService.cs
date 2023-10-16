using Domain.Abstractions;
using Domain.Models;

namespace Services;

public class BooksService : IBooksService
{
    private readonly IFileManager _fileManager;
    private readonly IUnitOfWork _unitOfWork;

    public BooksService(IUnitOfWork unitOfWork, IFileManager fileManager)
    {
        _unitOfWork = unitOfWork;
        _fileManager = fileManager;
    }
    public async Task<ListResponse<Book>> GetQueriedAsync(Query query)
    {
        return await _unitOfWork.Books.GetQueriedItemsAsync(query);
    }
    public async Task<Book> GetByIdAsync(int bookId)
    {
        return await _unitOfWork.Books.GetIncludingBookFilesAsync(bookId);
    }
    public async Task AddAsync(Book bookToCreate)
    {
        await _unitOfWork.Books.AddAsync(bookToCreate);
        await _unitOfWork.CompleteOrThrowAsync();
    }

    public async Task UpdateAsync(int bookId, Book bookToUpdate)
    {
        AssignId(bookId, bookToUpdate);
        _unitOfWork.Books.Update(bookToUpdate);
        await _unitOfWork.CompleteOrThrowAsync();
    }

    private void AssignId(int bookId, Book bookToUpdate)
    {
        bookToUpdate.Id = bookId;
    }
    public async Task RemoveAsync(Book book)
    {
        DeleteFileIfExists(book.BookFile);
        _unitOfWork.Books.Remove(book);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    private void DeleteFileIfExists(BookFile? bookFile)
    {
        if (bookFile != null)
            _fileManager.DeleteFile(bookFile.Url);
    }
}