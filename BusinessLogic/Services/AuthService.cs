using System.Security.Claims;
using Data.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Extensions;
using Services.ResponseDtos;

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
        _passwordManager.SecureUser(userToCreate);
        userToCreate.RoleId = await _unitOfWork.Roles.GetRoleIdByNameAsync(Roles.User);
        await _unitOfWork.Users.AddAsync(userToCreate);
        await _unitOfWork.CompleteAsync();
    }
    public async Task<AuthResult> GetAuthCredentialsAsync(User user)
    {
        var authResult = await _unitOfWork.Users
            .GetAll()
            .ToAuthResult()
            .FirstOrDefaultAsync(x => x.Username == user.Name);

        if (authResult is null)
            throw new EntityNotFoundException(typeof(User), nameof(User.Name), user.Name);
        
        _passwordManager.ThrowExceptionIfWrongPassword(user.Password, authResult.Password);
        authResult.Token = _tokenManager.GenerateToken(authResult.Username, authResult.Role);
        
        return authResult;
    }
    public async Task UpdateProfileAsync(User existingUser, User newUser, string? newPassword)
    {
        _passwordManager.ThrowExceptionIfWrongPassword(newUser.Password, existingUser.Password);
        ChangeUsername(existingUser, newUser.Name);
        SetNewPasswordIfPresent(existingUser, newPassword);
        await _unitOfWork.CompleteAsync();
    }

    private void ChangeUsername(User existingUser, string newUsername)
    {
        existingUser.Name = newUsername;
        _unitOfWork.Users.Update(existingUser);
    }
    private void SetNewPasswordIfPresent(User userInDb, string? newPassword)
    {
        if (newPassword != null)
        {
            _passwordManager.SecureUserWithNewPassword(userInDb, newPassword);
            _unitOfWork.Users.Update(userInDb);
        }
    }

    public async Task DeleteAccountAsync(User user, string inputtedPassword)
    {
        _passwordManager.ThrowExceptionIfWrongPassword(inputtedPassword, user.Password);
        _unitOfWork.Users.Remove(user);
        await _unitOfWork.CompleteAsync();
    }
    public string GetUsernameOrThrow(ClaimsPrincipal? claimsPrincipal)
    {
        var identity= claimsPrincipal?.Identity;
        var username = identity?.Name;
        var authenticated = identity?.IsAuthenticated ?? false;
        if (!authenticated)
            throw new UserNotAuthorizedException();
        if (username == null)
            throw new EntityNotFoundException(typeof(User), nameof(User.Name));
        return username;
    }
}