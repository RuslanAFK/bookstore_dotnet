using BookStoreServer.Resources.Auth;
using BookStoreServer.Resources.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IAuthService _authService;

    public AuthController(IUsersService usersService, IAuthService authService)
    {
        _usersService = usersService;
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginResource loginResource)
    {
        var user = loginResource.ToUser();
        var found = await _authService.GetAuthCredentialsAsync(user);
        var res = new
        {
            found.Id,
            found.Role,
            found.Token,
            found.Username
        };
        return Ok(res);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterResource registerResource)
    {
        var userToCreate = registerResource.ToUser();
        await _authService.RegisterAsync(userToCreate);
        return NoContent();
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UpdateUserInfoResource userInfoResource)
    {
        var username = User.Identity?.Name!;
        var foundUser = await _usersService.GetByNameAsync(username);
        var user = userInfoResource.ToUser();
        await _authService.UpdateProfileAsync(foundUser, user, userInfoResource.NewPassword);
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(DeleteUserResource resource)
    {
        var username = User.Identity?.Name!;
        var userToDelete = await _usersService.GetByNameAsync(username);
        await _authService.DeleteAccountAsync(userToDelete, resource.Password);
        return NoContent();
    }
}