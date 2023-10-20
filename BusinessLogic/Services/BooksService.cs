using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Dtos;
using Services.Extensions;

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
        var books = _unitOfWork.Books.GetAll().AsNoTracking();
        
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

    public async Task<GetSingleBookDto> GetSingleBookDtoByIdAsync(int bookId)
    {
        return await _unitOfWork.Books
                   .GetAll()
                   .AsNoTracking()
                   .ToGetSingleBookDto()
                   .FirstOrDefaultAsync(x => x.Id == bookId)
               ?? throw new EntityNotFoundException(typeof(Book), nameof(Book.Id));
    }

    public async Task AddAsync(Book bookToCreate)
    {
        await _unitOfWork.Books.AddAsync(bookToCreate);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(int bookId, BookDto updateBook)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId);
        book.Name = updateBook.Name;
        book.Info = updateBook.Info;
        book.Genre = updateBook.Genre;
        book.Image = updateBook.Image;
        book.Author = updateBook.Author;
        _unitOfWork.Books.Update(book);
        await _unitOfWork.CompleteAsync();
    }

    public async Task RemoveAsync(int bookId)
    {
        var book = await _unitOfWork.Books.GetByIdIncludingBookFilesAsync(bookId);
        DeleteFileIfExists(book.BookFile);
        _unitOfWork.Books.Remove(book);
        await _unitOfWork.CompleteAsync();
    }
    private void DeleteFileIfExists(BookFile? bookFile)
    {
        if (bookFile != null)
            _fileManager.DeleteFile(bookFile.Url);
    }
}