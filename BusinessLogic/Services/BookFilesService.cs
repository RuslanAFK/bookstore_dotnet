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

    public async Task AddAsync(Book book, IFormFile file)
    {
        DeleteFileIfExists(book.BookFile);

        var relativePath = await _fileManager.StoreFileAndGetPath(file);
        var bookFile = GenerateNewBookFile(book, relativePath);
        await _unitOfWork.BookFiles.AddAsync(bookFile);
        await _unitOfWork.CompleteOrThrowAsync();
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

    public async Task RemoveAsync(Book book)
    {
        var bookFile = GetBookFileOrThrow(book);
        _fileManager.DeleteFile(bookFile.Url);
        _unitOfWork.BookFiles.Remove(bookFile);
        await _unitOfWork.CompleteOrThrowAsync();
    }

    public void ThrowIfFileNotPermitted(IFormFile file)
    {
        var permittedType = ".pdf";
        if (file.Length == 0)
            throw new FileEmptyException();
        if (!file.FileName.EndsWith(permittedType))
            throw new UnsupportedFileTypeException(permittedType);
    }

    private BookFile GetBookFileOrThrow(Book book)
    {
        var bookFile = book.BookFile;
        if (bookFile == null)
            throw new EntityNotFoundException(typeof(Book), nameof(Book.BookFile));
        return bookFile;
    }
}