using AutoMapper;
using BookStoreServer.Resources.Auth;
using BookStoreServer.Resources.Users;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.ResponseDtos;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;
    private readonly IAuthService _authService;

    public AuthController(IMapper mapper, IUsersService usersService, IAuthService authService)
    {
        _mapper = mapper;
        _usersService = usersService;
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginResource loginResource)
    {
        var user = _mapper.Map<LoginResource, User>(loginResource);
        var found = await _authService.GetAuthCredentialsAsync(user);
        var res = _mapper.Map<AuthResult, AuthResultResource>(found);
        return Ok(res);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterResource registerResource)
    {
        var userToCreate = _mapper.Map<RegisterResource, User>(registerResource);
        await _authService.RegisterAsync(userToCreate);
        return NoContent();
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UpdateUserInfoResource userInfoResource)
    {
        var claimsPrincipal = HttpContext?.User;
        var username = _authService.GetUsernameOrThrow(claimsPrincipal);
        var foundUser = await _usersService.GetByNameAsync(username);
        var user = _mapper.Map<UpdateUserInfoResource, User>(userInfoResource);
        await _authService.UpdateProfileAsync(foundUser, user, userInfoResource.NewPassword);
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(DeleteUserResource resource)
    {
        var claimsPrincipal = HttpContext?.User;
        var username = _authService.GetUsernameOrThrow(claimsPrincipal);
        var userToDelete = await _usersService.GetByNameAsync(username);
        await _authService.DeleteAccountAsync(userToDelete, resource.Password);
        return NoContent();
    }
}