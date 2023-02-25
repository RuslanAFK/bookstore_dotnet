using System.Security.Claims;
using AutoMapper;
using BookStoreServer.Controllers.Resources.Auth;
using BookStoreServer.Controllers.Resources.Users;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IUsersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenManager _tokenManager;

    public AuthController(IUsersRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
        ITokenManager tokenManager)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenManager = tokenManager;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginResource loginResource)
    {
        var user = _mapper.Map<LoginResource, User>(loginResource);
        var foundUser = await _repository.CheckCredentialsAsync(user);
        if (foundUser == null)
            return NotFound();
        if (!BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password)) 
            return BadRequest("Provided incorrect password.");
        var roleName = await _repository.GetRoleById(foundUser.RoleId);
        var token = _tokenManager.GenerateToken(foundUser, roleName);
        return Ok(new AuthResult
        {
            Id = foundUser.Id,
            Username = foundUser.Name,
            Token = token,
            Role = roleName
        });
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterResource registerResource)
    {
        registerResource.Password = BCrypt.Net.BCrypt.HashPassword(registerResource.Password);
        try
        {  
            var userToCreate = _mapper.Map<RegisterResource, User>(registerResource);
            _repository.Signup(userToCreate);
            await _repository.AddUserToRole(userToCreate, false);
            var createSuccessful = await _unitOfWork.CompleteAsync();
            if (createSuccessful > 0)
                return NoContent();
            return BadRequest();
        }
        catch (DbUpdateException e)
        {
            var inner = e.InnerException;
            return BadRequest(inner==null ? e.Message : inner.Message);
        }
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UpdateUserInfoResource userInfoResource)
    {
        var username = (HttpContext.User.Identity as ClaimsIdentity)?.Name;
        if (username == null)
            return BadRequest();
        try
        {
            var foundUser = await _repository.GetUserByNameAsync(username);
            if (foundUser == null)
                return NotFound();
            if (!BCrypt.Net.BCrypt.Verify(userInfoResource.Password, foundUser.Password)) 
                return BadRequest("Provided incorrect password.");
            _mapper.Map(userInfoResource, foundUser);
            foundUser.Password = BCrypt.Net.BCrypt
                .HashPassword(userInfoResource.NewPassword ?? userInfoResource.Password);
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
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteMyself(DeleteUserResource resource)
    {
        var username = (HttpContext.User.Identity as ClaimsIdentity)?.Name;
        if (username == null)
            return BadRequest();
        try
        {
            var userToDelete = await _repository.GetUserByNameAsync(username);
            if (userToDelete == null)
                return NotFound();
            if (!BCrypt.Net.BCrypt.Verify(resource.Password, userToDelete.Password)) 
                return BadRequest("Provided incorrect password.");
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