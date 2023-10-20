using Data.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Dtos;
using Services.Extensions;

namespace Services.Services;

public class AuthService : IAuthService
{
    private readonly ITokenManager _tokenManager;
    private readonly IPasswordManager _passwordManager;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(ITokenManager tokenManager, 
        IUnitOfWork unitOfWork, IPasswordManager passwordManager)
    {
        _tokenManager = tokenManager;
        _unitOfWork = unitOfWork;
        _passwordManager = passwordManager;
    }
    public async Task RegisterAsync(User userToCreate)
    {
        userToCreate.Password = _passwordManager.SecureUser(userToCreate);
        userToCreate.Role = await _unitOfWork.Roles.GetByNameAsync(Roles.User);
        await _unitOfWork.Users.AddAsync(userToCreate);
        await _unitOfWork.CompleteAsync();
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
    public async Task UpdateProfileAsync(User existingUser, User newUser, string? newPassword)
    {
        _passwordManager.CheckPassword(newUser.Password, existingUser.Password);
        existingUser.Name = newUser.Name;
        if (newPassword != null)
            existingUser.Password = _passwordManager.SecureUserWithNewPassword(existingUser, newPassword);
        _unitOfWork.Users.Update(existingUser);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAccountAsync(User user, string inputtedPassword)
    {
        _passwordManager.CheckPassword(inputtedPassword, user.Password);
        _unitOfWork.Users.Remove(user);
        await _unitOfWork.CompleteAsync();
    }
}