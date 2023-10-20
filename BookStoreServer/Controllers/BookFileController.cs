using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookFileController : Controller
{
    private readonly IBookFilesService _bookFilesService;

    public BookFileController(IBookFilesService bookFilesService)
    {
        _bookFilesService = bookFilesService;
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(int bookId, IFormFile file)
    {
        _bookFilesService.ThrowIfFileNotPermitted(file);
        await _bookFilesService.AddAsync(bookId, file);
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        await _bookFilesService.RemoveAsync(bookId);
        return NoContent();
    }
}