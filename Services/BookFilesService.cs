﻿using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Services;

public class BookFilesService : BaseService, IBookFilesService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IBaseRepository<BookFile> _bookFilesRepository;
    public BookFilesService(IUnitOfWork unitOfWork, IFileStorageService fileStorageService,
        IBaseRepository<BookFile> bookFilesRepository) : base(unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _bookFilesRepository = bookFilesRepository;
    }


    public async Task AddAsync(Book book, IFormFile file)
    {
        DeleteFileIfExists(book.BookFile);

        var relativePath = await _fileStorageService.StoreFileAndGetPath(file);
        var bookFile = GenerateNewBookFile(book, relativePath);
        await _bookFilesRepository.AddAsync(bookFile);
        await CompleteAndCheckIfCompleted();
    }

    private void DeleteFileIfExists(BookFile? bookFile)
    {
        if (bookFile != null)
            _fileStorageService.DeleteFile(bookFile.Url);
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

        _fileStorageService.DeleteFile(bookFile.Url);

        _bookFilesRepository.Remove(bookFile);
        await CompleteAndCheckIfCompleted();
    }

    public void CheckIfFilePermitted(IFormFile file)
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