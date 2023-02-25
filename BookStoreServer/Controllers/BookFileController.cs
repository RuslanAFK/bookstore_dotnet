using BookStoreServer.Core.Services;
using BookStoreServer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookFileController : Controller
{
    private readonly IBooksRepository _booksRepository;
    private readonly IBookFileService _bookFileService;
    private readonly IUnitOfWork _unitOfWork;

    public BookFileController(IBooksRepository booksRepository, IBookFileService bookFileService, IUnitOfWork unitOfWork)
    {
        _booksRepository = booksRepository;
        _bookFileService = bookFileService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(int bookId, IFormFile file)
    {
        var book  = await _booksRepository.GetBookByIdAsync(bookId);
        if (book == null)
            return NotFound();
        if (file.Length == 0)
            return BadRequest("Empty file is not permitted.");
        if (!file.FileName.EndsWith("pdf"))
            return BadRequest($"Unsupported file type. Supported is only pdf.");

        try
        {
            await _bookFileService.AddBookFile(book, file);
            var res = await _unitOfWork.CompleteAsync();
            if (res <= 0)
                return BadRequest();
            return NoContent();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }
    
    [HttpDelete]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        var book  = await _booksRepository.GetBookByIdAsync(bookId);
        if (book?.BookFile == null)
            return NotFound();
        
        try
        {
            await _bookFileService.RemoveBookFile(book);
            var res = await _unitOfWork.CompleteAsync();
            if (res <= 0)
                return BadRequest();
            return NoContent();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }
}