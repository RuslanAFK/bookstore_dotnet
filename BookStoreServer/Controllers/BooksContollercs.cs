using BookStoreServer.Core;
using BookStoreServer.Core.Models;
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
    public async Task<IActionResult> All()
    {
        var books = await _repository.GetBooksAsync();
        return Ok(books);
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> Get(int bookId)
    {
        var bookToReturn = await _repository.GetBookByIdAsync(bookId);
        if (bookToReturn != null)
            return Ok(bookToReturn);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book bookToCreate)
    {
        try
        {
            await _repository.CreateBookAsync(bookToCreate);
            var createSuccessful = await _unitOfWork.CompleteAsync();
            if (createSuccessful > 0)
                return CreatedAtAction("Get", new {id=bookToCreate.Id}, bookToCreate);
            return BadRequest();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }

    [HttpPut]
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