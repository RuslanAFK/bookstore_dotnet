using AutoMapper;
using BookStoreServer.Controllers.Resources.Books;
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
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthRepository _authRepository;

    public UsersController(IUsersRepository usersRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IAuthRepository authRepository)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authRepository = authRepository;
    }
    
    [HttpGet]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> All([FromQuery] QueryObject queryObject)
    {
        var users = await _usersRepository.GetUsersAsync(queryObject);
        var res = 
            _mapper.Map<ListResponse<User>, ListResponseResource<GetUsersResource>>(users);
        return Ok(res);
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Get(int bookId)
    {
        var userToReturn = await _usersRepository.GetUserByIdAsync(bookId);
        if (userToReturn == null)
            return NotFound();
        
        var res = _mapper.Map<User, GetUsersResource>(userToReturn);
        return Ok(res);
    }
    
    [HttpPut]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> UpdateRole(UpdateUserResource userResource)
    {
        try
        {
            var foundUser = await _usersRepository.GetUserByIdAsync(userResource.Id);
            if (foundUser == null)
                return NotFound();
            var isAdmin = userResource.RoleName == "Admin";
            await _authRepository.AddToRole(foundUser, isAdmin);
            _mapper.Map(userResource, foundUser);
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
    
    [HttpDelete("{userId}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Delete(int userId)
    {
        try
        {
            var userToDelete = await _usersRepository.GetUserByIdAsync(userId);
            if (userToDelete == null)
                return NotFound();
            _usersRepository.RemoveUser(userToDelete);
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