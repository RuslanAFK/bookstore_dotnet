using Domain.Constants;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Dtos;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpGet]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> GetQueried([FromQuery] Query query)
    {
        var users = await _usersService.GetQueriedAsync(query);
        return Ok(users);
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> GetById(int bookId)
    {
        var userToReturn = await _usersService.GetUserDtoByIdAsync(bookId);
        return Ok(userToReturn);
    }
    
    [HttpPatch("{id:int}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> UpdateRole(int id, UserRoleDto userRoleDto)
    {
        await _usersService.AddUserToRoleAsync(id, userRoleDto.RoleName);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> Delete(int userId)
    {
        await _usersService.RemoveAsync(userId);
        return NoContent();
    }
}