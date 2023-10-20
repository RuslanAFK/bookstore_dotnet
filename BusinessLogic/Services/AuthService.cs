using Data.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Services.Dtos;
using Services.Extensions;

namespace Services.Services;

public class AuthService : IAuthService
{
    private readonly ITokenManager _tokenManager;
    private readonly IPasswordManager _passwordManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    public AuthService(ITokenManager tokenManager, 
        IUnitOfWork unitOfWork, IPasswordManager passwordManager, IConfiguration configuration)
    {
        _tokenManager = tokenManager;
        _unitOfWork = unitOfWork;
        _passwordManager = passwordManager;
        _configuration = configuration;
    }
    public async Task RegisterUserAsync(User userToCreate)
    {
        await RegisterAsync(userToCreate, Roles.User);
    }
    public async Task<AuthResult> GetAuthCredentialsAsync(User user)
    {
        var authResult = await _unitOfWork.Users
            .GetAll()
            .ToAuthResult()
            .FirstOrDefaultAsync(x => x.Username == user.Name) 
            ?? throw new EntityNotFoundException(typeof(User), nameof(User.Name), user.Name);
        
        _passwordManager.CheckPassword(user.Password, authResult.Password);
        authResult.Token = _tokenManager.GenerateToken(authResult.Username, authResult.Role);
        
        return authResult;
    }
    public async Task UpdateProfileAsync(string username, User newUser, string? newPassword)
    {
        var foundUser = await _unitOfWork.Users.GetByNameAsync(username);
        _passwordManager.CheckPassword(newUser.Password, foundUser.Password);
        foundUser.Name = newUser.Name;
        if (newPassword != null)
            foundUser.Password = _passwordManager.SecureUserWithNewPassword(foundUser, newPassword);
        _unitOfWork.Users.Update(foundUser);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAccountAsync(string username, string inputtedPassword)
    {
        var foundUser = await _unitOfWork.Users.GetByNameAsync(username);
        _passwordManager.CheckPassword(inputtedPassword, foundUser.Password);
        _unitOfWork.Users.Remove(foundUser);
        await _unitOfWork.CompleteAsync();
    }
    
    public async Task AddDefaultCreator()
    {
        if (!await _unitOfWork.Users.CreatorExists())
        {
            var name = _configuration["Secrets:CreatorName"]!;
            var password = _configuration["Secrets:CreatorPass"]!;
            var defaultCreator = new User
            {
                Name = name,
                Password = password,
            };
            await RegisterAsync(defaultCreator, Roles.Creator);
        }
    }
    private async Task RegisterAsync(User userToCreate, string role)
    {
        userToCreate.Password = _passwordManager.SecureUser(userToCreate);
        userToCreate.Role = await _unitOfWork.Roles.GetByNameAsync(role);
        await _unitOfWork.Users.AddAsync(userToCreate);
        await _unitOfWork.CompleteAsync();
    }
}