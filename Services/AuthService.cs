using System.Security.Claims;
using System.Security.Principal;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;
using Domain.StaticManagers;

namespace Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly ITokenManager _tokenManager;

    public AuthService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokenManager tokenManager, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _usersRepository = usersRepository;
        _rolesRepository = rolesRepository;
        _tokenManager = tokenManager;
    }

    public async Task RegisterAsync(User userToCreate)
    {
        var securedUser = PasswordManager.GetSecuredUser(userToCreate);
        await _usersRepository.AddAsync(securedUser);
        await _rolesRepository.AssignToRole(securedUser, Roles.User);
        await CompleteAndCheckIfCompleted();
    }

    public async Task<AuthResult> GetAuthCredentials(User user)
    {
        var foundUser = await _usersRepository.GetByNameAsync(user.Name);
        PasswordManager.CheckPassword(user.Password, foundUser.Password);
        var roleName = await _rolesRepository.GetRoleNameByIdAsync(foundUser.RoleId);
        var token = _tokenManager.GenerateToken(foundUser, roleName);
        return new AuthResult(foundUser, token, roleName);
    }

    public async Task UpdateProfileAsync(User foundUser, User user, string? newPassword)
    {
        PasswordManager.CheckPassword(user.Password, foundUser.Password);
        foundUser.Name = user.Name;
        if (newPassword != null)
            foundUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await CompleteAndCheckIfCompleted();
    }

    public async Task DeleteAccountAsync(User user, string inputtedPassword)
    {
        PasswordManager.CheckPassword(inputtedPassword, user.Password);
        _usersRepository.Remove(user);
        await CompleteAndCheckIfCompleted();
    }

    public string GetUsernameOrThrow(IIdentity? identity)
    {
        var username = (identity as ClaimsIdentity)?.Name;
        if (username == null)
            throw new UserNotAuthorizedException();
        return username;
    }
}