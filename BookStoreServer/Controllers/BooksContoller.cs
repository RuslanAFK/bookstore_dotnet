using Domain.Constants;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Dtos;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : Controller
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetQueried([FromQuery] Query query)
    {
        var books = await _booksService.GetQueriedAsync(query);
        return Ok(books);
    }

    [HttpGet("{bookId}")]
    [Authorize]
    public async Task<IActionResult> GetById(int bookId)
    {
        var bookToReturn = await _booksService.GetSingleBookDtoByIdAsync(bookId);
        return Ok(bookToReturn);
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(BookDto bookDto)
    {
        var bookToCreate = bookDto.ToBook();
        await _booksService.AddAsync(bookToCreate);
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Update(int id, BookDto bookDto)
    {
        await _booksService.UpdateAsync(id, bookDto);
        return NoContent();
    }

    [HttpDelete("{bookId}")]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        await _booksService.RemoveAsync(bookId);
        return NoContent();
    }
}