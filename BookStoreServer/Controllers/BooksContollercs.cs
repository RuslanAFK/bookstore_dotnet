using AutoMapper;
using BookStoreServer.Controllers.Resources.Books;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Models;
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
    public async Task<IActionResult> GetQueried([FromQuery] Query query)
    {
        var books = await _booksService.GetQueriedAsync(query);
        var res = 
            _mapper.Map<ListResponse<Book>, ListResponseResource<GetBooksResource>>(books);
        return Ok(res);
    }

    [HttpGet("{bookId}")]
    [Authorize]
    public async Task<IActionResult> GetById(int bookId)
    {
        var bookToReturn = await _booksService.GetByIdAsync(bookId);
        var res = _mapper.Map<Book, GetSingleBookResource>(bookToReturn);
        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Create(CreateBookResource bookResource)
    {
        var bookToCreate = _mapper.Map<CreateBookResource, Book>(bookResource);
        await _booksService.AddAsync(bookToCreate);
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Update(int id, CreateBookResource bookResource)
    {
        var bookToUpdate = _mapper.Map<CreateBookResource, Book>(bookResource);
        await _booksService.UpdateAsync(id, bookToUpdate);
        return NoContent();
    }

    [HttpDelete("{bookId}")]
    [Authorize(Roles = Roles.AdminAndCreator)]
    public async Task<IActionResult> Delete(int bookId)
    {
        var bookToDelete = await _booksService.GetByIdAsync(bookId);
        await _booksService.RemoveAsync(bookToDelete);
        return NoContent();
    }
}