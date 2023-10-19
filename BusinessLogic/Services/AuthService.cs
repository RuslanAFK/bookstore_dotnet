using System.Security.Claims;
using Data.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;

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
        await _unitOfWork.Users.AddAsync(userToCreate);
        await _unitOfWork.Roles.AssignToRoleAsync(userToCreate, Roles.User);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    public async Task<AuthResult> GetAuthCredentialsAsync(User user)
    {
        var foundUser = await _unitOfWork.Users.GetByNameAsync(user.Name);
        _passwordManager.ThrowExceptionIfWrongPassword(user.Password, foundUser.Password);
        var roleName = await _unitOfWork.Roles.GetRoleNameByIdAsync(foundUser.RoleId);
        var token = _tokenManager.GenerateToken(foundUser, roleName);
        return new AuthResult(foundUser, token, roleName);
    }
    public async Task UpdateProfileAsync(User existingUser, User newUser, string? newPassword)
    {
        _passwordManager.ThrowExceptionIfWrongPassword(newUser.Password, existingUser.Password);
        ChangeUsername(existingUser, newUser.Name);
        SetNewPasswordIfPresent(existingUser, newPassword);
        await _unitOfWork.CompleteOrThrowAsync();
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
        await _unitOfWork.CompleteOrThrowAsync();
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