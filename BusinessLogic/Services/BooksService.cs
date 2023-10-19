using Data.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Extensions;
using Services.ResponseDtos;

namespace Services.Services;

public class BooksService : IBooksService
{
    private readonly IFileManager _fileManager;
    private readonly IUnitOfWork _unitOfWork;

    public BooksService(IUnitOfWork unitOfWork, IFileManager fileManager)
    {
        _unitOfWork = unitOfWork;
        _fileManager = fileManager;
    }
    public async Task<ListResponse<GetBooksDto>> GetQueriedAsync(Query query)
    {
        var books = _unitOfWork.Books.GetAll();
        
        var itemsSearched = books
            .ApplySearching(query);
        var itemsPaginatedList = await itemsSearched
            .ApplyPagination(query, 4)
            .ToGetBooksDto()
            .ToListAsync();
        var itemsCount = itemsSearched.Count();
        
        return new ListResponse<GetBooksDto>
        {
            Items = itemsPaginatedList,
            Count = itemsCount
        };
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