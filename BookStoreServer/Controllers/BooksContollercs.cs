using AutoMapper;
using BookStoreServer.Controllers.Resources.Books;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using BookStoreServer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBooksService _booksService;

    public BooksController(IMapper mapper, IBooksService booksService)
    {
        _mapper = mapper;
        _booksService = booksService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> All([FromQuery] QueryObject queryObject)
    {
        var books = await _booksService.GetBooksAsync(queryObject);
        var res = 
            _mapper.Map<ListResponse<Book>, ListResponseResource<GetBooksResource>>(books);
        return Ok(res);
    }

    [HttpGet("{bookId}")]
    [Authorize]
    public async Task<IActionResult> Get(int bookId)
    {
        var bookToReturn = await _booksService.GetBookByIdAsync(bookId);
        if (bookToReturn == null)
            return NotFound();
        var res = _mapper.Map<Book, GetSingleBookResource>(bookToReturn);
        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(CreateBookResource bookResource)
    {
        var bookToCreate = _mapper.Map<CreateBookResource, Book>(bookResource);
        var createSuccessful = await _booksService.CreateBookAsync(bookToCreate);
        if (createSuccessful) 
            return NoContent();
        return BadRequest();
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Update(int id, CreateBookResource bookResource)
    {
        var bookToUpdate = _mapper.Map<CreateBookResource, Book>(bookResource);
        var updateSuccessful = await _booksService.UpdateBookAsync(id, bookToUpdate);
        if (updateSuccessful) 
            return NoContent();
        return BadRequest();
    }

    [HttpDelete("{bookId}")]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        var bookToDelete = await _booksService.GetBookByIdAsync(bookId);
        if (bookToDelete == null)
            return NotFound();
        var success = await _booksService.DeleteBookAsync(bookToDelete);
        if (success)
            return NoContent();
        return BadRequest();
    }
}