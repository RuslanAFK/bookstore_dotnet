using BookStoreServer.Core;
using BookStoreServer.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : Controller
{
    private readonly IBooksRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public BooksController(IBooksRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> All([FromQuery] BookQuery query)
    {
        var books = await _repository.GetBooksAsync(query);
        return Ok(books);
    }

    [HttpGet("{bookId}")]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Get(int bookId)
    {
        var bookToReturn = await _repository.GetBookByIdAsync(bookId);
        if (bookToReturn != null)
            return Ok(bookToReturn);
        return NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Book bookToCreate)
    {
        try
        {
            await _repository.CreateBookAsync(bookToCreate);
            var createSuccessful = await _unitOfWork.CompleteAsync();
            if (createSuccessful > 0)
                return NoContent();
            return BadRequest();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Book bookToUpdate)
    {
        try
        {
            _repository.UpdateBook(bookToUpdate);
            var updateSuccessful = await _unitOfWork.CompleteAsync();
            if (updateSuccessful > 0)
                return NoContent();
            return BadRequest();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }

    [HttpDelete("{bookId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int bookId)
    {
        try
        {
            var bookToDelete = await _repository.GetBookByIdAsync(bookId);
            if (bookToDelete == null)
                return NotFound();
            _repository.DeleteBook(bookToDelete);
            var success = await _unitOfWork.CompleteAsync();
            if (success > 0)
                return NoContent();
            return BadRequest();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }
}