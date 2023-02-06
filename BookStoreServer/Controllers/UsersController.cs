using AutoMapper;
using BookStoreServer.Controllers.Resources;
using BookStoreServer.Controllers.Resources.Users;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUsersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersController(IUsersRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> All([FromQuery] QueryObject queryObject)
    {
        var users = await _repository.GetUsersAsync(queryObject);
        var res = 
            _mapper.Map<ListResponse<User>, ListResponseResource<GetUsersResource>>(users);
        return Ok(res);
    }

    [HttpGet("{bookId}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Get(int bookId)
    {
        var userToReturn = await _repository.GetUserByIdAsync(bookId);
        if (userToReturn == null)
            return NotFound();
        
        var res = _mapper.Map<User, GetUsersResource>(userToReturn);
        return Ok(res);
    }
    
    
}