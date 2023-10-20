using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Services.Abstractions;

namespace Services.Services;

public class BookFilesService : IBookFilesService
{
    private readonly IFileManager _fileManager;
    private readonly IUnitOfWork _unitOfWork;
    public BookFilesService(IUnitOfWork unitOfWork, IFileManager fileManager)
    {
        _unitOfWork = unitOfWork;
        _fileManager = fileManager;
    }

    public async Task AddAsync(int bookId, IFormFile file)
    {
        var book = await _unitOfWork.Books.GetByIdIncludingBookFilesAsync(bookId);
        DeleteFileIfExists(book.BookFile);

        var relativePath = await _fileManager.StoreFileAndGetPath(file);
        var bookFile = GenerateNewBookFile(book, relativePath);
        await _unitOfWork.BookFiles.AddAsync(bookFile);
        await _unitOfWork.CompleteAsync();
    }

    private void DeleteFileIfExists(BookFile? bookFile)
    {
        if (bookFile != null)
            _fileManager.DeleteFile(bookFile.Url);
    }

    private BookFile GenerateNewBookFile(Book book, string relativePath)
    {
        return new BookFile
        {
            Book = book, Url = relativePath
        };
    }

    public async Task RemoveAsync(int bookId)
    {
        var book = await _unitOfWork.Books.GetByIdIncludingBookFilesAsync(bookId);
        var bookFile = book.BookFile ?? throw new EntityNotFoundException(typeof(Book), nameof(Book.BookFile));
        _fileManager.DeleteFile(bookFile.Url);
        _unitOfWork.BookFiles.Remove(bookFile);
        await _unitOfWork.CompleteAsync();
    }

    public void ThrowIfFileNotPermitted(IFormFile file)
    {
        var permittedType = ".pdf";
        if (file.Length == 0)
            throw new FileEmptyException();
        if (!file.FileName.EndsWith(permittedType))
            throw new UnsupportedFileTypeException(permittedType);
    }
}