using System.Net;
using AutoMapper;
using BookStoreServer.Controllers.Resources.Books;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using BookStoreServer.Enums;
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
    private readonly IMapper _mapper;

    public BooksController(IBooksRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = AuthSchemes.Asymmetric)]
    public async Task<IActionResult> All([FromQuery] QueryObject queryObject)
    {
        var books = await _repository.GetBooksAsync(queryObject);
        var res = 
            _mapper.Map<ListResponse<Book>, ListResponseResource<GetBooksResource>>(books);
        return Ok(res);
    }

    [HttpGet("{bookId}")]
    [Authorize(AuthenticationSchemes = AuthSchemes.Asymmetric)]
    public async Task<IActionResult> Get(int bookId)
    {
        var bookToReturn = await _repository.GetBookByIdAsync(bookId);
        if (bookToReturn == null)
            return NotFound();
        var res = _mapper.Map<Book, GetSingleBookResource>(bookToReturn);
        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Creator}", AuthenticationSchemes = AuthSchemes.Asymmetric)]
    public async Task<IActionResult> Create(CreateBookResource bookResource)
    {
        var bookToCreate = _mapper.Map<CreateBookResource, Book>(bookResource);
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
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Creator}", AuthenticationSchemes = AuthSchemes.Asymmetric)]
    public async Task<IActionResult> Update(UpdateBookResource bookResource)
    {
        var bookToUpdate = _mapper.Map<UpdateBookResource, Book>(bookResource);
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
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Creator}", AuthenticationSchemes = AuthSchemes.Asymmetric)]
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