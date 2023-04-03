using AutoMapper;
using BookStoreServer.Controllers.Resources.Books;
using BookStoreServer.Controllers.Resources.Users;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using BookStoreServer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public UsersController(IMapper mapper, IUsersService usersService)
    {
        _mapper = mapper;
        _usersService = usersService;
    }
    
    [HttpGet]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> All([FromQuery] QueryObject queryObject)
    {
        var users = await _usersService.GetUsersAsync(queryObject);
        var res = 
            _mapper.Map<ListResponse<User>, ListResponseResource<GetUsersResource>>(users);
        return Ok(res);
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> Get(int bookId)
    {
        var userToReturn = await _usersService.GetUserByIdAsync(bookId);
        if (userToReturn == null)
            return NotFound();
        
        var res = _mapper.Map<User, GetUsersResource>(userToReturn);
        return Ok(res);
    }
    
    [HttpPatch("{id:int}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> UpdateRole(int id, UserRoleResource userRoleResource)
    {
        var foundUser = await _usersService.GetUserByIdAsync(id);
        if (foundUser == null)
            return NotFound();
        var updated = await _usersService.AddUserToRoleAsync(foundUser, userRoleResource.RoleName);
        if (updated)
            return NoContent();
        return BadRequest();
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> Delete(int userId)
    {
        var userToDelete = await _usersService.GetUserByIdAsync(userId);
        if (userToDelete == null)
            return NotFound();
        var success = await _usersService.RemoveUserAsync(userToDelete);
        if (success)
            return NoContent();
        return BadRequest();
    }    
}