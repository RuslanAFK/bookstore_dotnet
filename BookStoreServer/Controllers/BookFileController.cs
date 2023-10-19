using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookFileController : Controller
{
    private readonly IBooksService _booksService;
    private readonly IBookFilesService _bookFilesService;

    public BookFileController(IBooksService booksService, IBookFilesService bookFilesService)
    {
        _booksService = booksService;
        _bookFilesService = bookFilesService;
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(int bookId, IFormFile file)
    {
        _bookFilesService.ThrowIfFileNotPermitted(file);
        var book = await _booksService.GetByIdIncludingFilesAsync(bookId);
        await _bookFilesService.AddAsync(book, file);
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        var book = await _booksService.GetByIdIncludingFilesAsync(bookId);
        await _bookFilesService.RemoveAsync(book);
        return NoContent();
    }
}