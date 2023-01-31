using AutoMapper;
using BookStoreServer.Controllers.Resources;
using BookStoreServer.Core;
using BookStoreServer.Core.Models;
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

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginResource loginResource)
    {
        var user = _mapper.Map<LoginResource, User>(loginResource);
        var foundUser = await _repository.LoginAsync(user);
        if (foundUser == null)
            return NotFound();
        var result = _mapper.Map<User, GetUserResource>(user);
        return Ok(result);
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup(SignupResource signupResource)
    {
        try
        {
            var userToCreate = _mapper.Map<SignupResource, User>(signupResource);
            _repository.Signup(userToCreate);
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