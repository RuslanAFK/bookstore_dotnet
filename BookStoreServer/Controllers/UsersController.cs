using AutoMapper;
using BookStoreServer.Controllers.Resources.Books;
using BookStoreServer.Controllers.Resources.Users;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Models;
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
    public async Task<IActionResult> GetQueried([FromQuery] Query query)
    {
        var users = await _usersService.GetQueriedAsync(query);
        var res = 
            _mapper.Map<ListResponse<User>, ListResponseResource<GetUsersResource>>(users);
        return Ok(res);
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> GetById(int bookId)
    {
        var userToReturn = await _usersService.GetByIdAsync(bookId);

        var res = _mapper.Map<User, GetUsersResource>(userToReturn);
        return Ok(res);
    }
    
    [HttpPatch("{id:int}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> UpdateRole(int id, UserRoleResource userRoleResource)
    {
        var foundUser = await _usersService.GetByIdAsync(id);
        await _usersService.AddUserToRoleAsync(foundUser, userRoleResource.RoleName);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = Roles.Creator)]
    public async Task<IActionResult> Delete(int userId)
    {
        var userToDelete = await _usersService.GetByIdAsync(userId);
        await _usersService.RemoveAsync(userToDelete);
        return NoContent();
    }
}