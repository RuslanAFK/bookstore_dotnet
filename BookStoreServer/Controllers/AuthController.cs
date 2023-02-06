using AutoMapper;
using BookStoreServer.Controllers.Resources.Auth;
using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenManager _tokenManager;

    public AuthController(IAuthRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
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
        var roleName = await _repository.GetUserRole(foundUser.RoleId);
        var token = _tokenManager.GenerateToken(foundUser, roleName);
        return Ok(new AuthResult
        {
            Username = foundUser.Username,
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
            await _repository.AddToRole(userToCreate, false);
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
}