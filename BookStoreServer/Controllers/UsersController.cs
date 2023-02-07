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

    [HttpGet("{userId}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Get(int bookId)
    {
        var userToReturn = await _repository.GetUserByIdAsync(bookId);
        if (userToReturn == null)
            return NotFound();
        
        var res = _mapper.Map<User, GetUsersResource>(userToReturn);
        return Ok(res);
    }
    
    [HttpPut]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> UpdateRole(UpdateUserRoleResource userRoleResource)
    {
        try
        {
            var foundUser = await _repository.GetUserByIdAsync(userRoleResource.Id);
            if (foundUser == null)
                return NotFound();
            var isAdmin = userRoleResource.RoleName == "Admin";
            await _repository.AddUserToRole(foundUser, isAdmin);
            _mapper.Map(userRoleResource, foundUser);
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
            var userToDelete = await _repository.GetUserByIdAsync(userId);
            if (userToDelete == null)
                return NotFound();
            _repository.RemoveUser(userToDelete);
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