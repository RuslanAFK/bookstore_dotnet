using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Dtos;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = loginDto.ToUser();
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
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var userToCreate = registerDto.ToUser();
        await _authService.RegisterUserAsync(userToCreate);
        return NoContent();
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UpdateUserDto userDto)
    {
        var username = User?.Identity?.Name ?? "";
        var user = userDto.ToUser();
        await _authService.UpdateProfileAsync(username, user, userDto.NewPassword);
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(DeleteUserDto dto)
    {
        var username = User?.Identity?.Name ?? "";
        await _authService.DeleteAccountAsync(username, dto.Password);
        return NoContent();
    }
}