using System.Security.Claims;
using AutoMapper;
using BookStoreServer.Controllers.Resources.Auth;
using BookStoreServer.Controllers.Resources.Users;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public AuthController(IMapper mapper, IUsersService usersService)
    {
        _mapper = mapper;
        _usersService = usersService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginResource loginResource)
    {
        var user = _mapper.Map<LoginResource, User>(loginResource);
        var found = await _usersService.GetAuthResultAsync(user);
        if (found == null)
            return NotFound();
        var res = _mapper.Map<AuthResult, AuthResultResource>(found);
        return Ok(res);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterResource registerResource)
    {
        var userToCreate = _mapper.Map<RegisterResource, User>(registerResource);
        var createSuccessful = await _usersService.RegisterAsync(userToCreate);
        if (createSuccessful)
            return NoContent();
        return BadRequest();
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UpdateUserInfoResource userInfoResource)
    {
        var username = (HttpContext.User.Identity as ClaimsIdentity)?.Name;
        if (username == null)
            return BadRequest();
        var foundUser = await _usersService.GetUserByNameAsync(username);
        if (foundUser == null)
            return NotFound();
        var user = _mapper.Map<UpdateUserInfoResource, User>(userInfoResource);;
        var updateSuccessful = await _usersService.UpdateProfileAsync(foundUser, user, userInfoResource.NewPassword);
        if (updateSuccessful)
            return NoContent();
        return BadRequest();
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(DeleteUserResource resource)
    {
        var username = (HttpContext.User.Identity as ClaimsIdentity)?.Name;
        if (username == null)
            return BadRequest();
        var userToDelete = await _usersService.GetUserByNameAsync(username);
        if (userToDelete == null)
            return NotFound();
        var success = await _usersService.DeleteAccountAsync(userToDelete, resource.Password);
        if (success)
            return NoContent();
        return BadRequest();
    }
}