using BookStoreServer.Core.Services;
using BookStoreServer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookFileController : Controller
{
    private readonly IBooksService _booksService;

    public BookFileController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(int bookId, IFormFile file)
    {
        if (file.Length == 0)
            return BadRequest("Empty file is not permitted.");
        if (!file.FileName.EndsWith("pdf"))
            return BadRequest($"Unsupported file type. Supported is only pdf.");
        var book  = await _booksService.GetBookByIdAsync(bookId);
        if (book == null)
            return NotFound();
        var added = await _booksService.AddFileToBookAsync(book, file);
        if (added)
            return NoContent();
        return BadRequest();
    }
    
    [HttpDelete]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        var book  = await _booksService.GetBookByIdAsync(bookId);
        if (book?.BookFile == null)
            return NotFound();
        var removed = await _booksService.RemoveFileFromBookAsync(book);
        if (removed)
            return NoContent();
        return BadRequest();
    }
}