using System.Security.Claims;
using Domain.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;

namespace Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly ITokenManager _tokenManager;
    private readonly IPasswordManager _passwordManager;

    public AuthService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokenManager tokenManager, 
        IUnitOfWork unitOfWork, IPasswordManager passwordManager) : base(unitOfWork)
    {
        _usersRepository = usersRepository;
        _rolesRepository = rolesRepository;
        _tokenManager = tokenManager;
        _passwordManager = passwordManager;
    }
    public async Task RegisterAsync(User userToCreate)
    {
        _passwordManager.SecureUser(userToCreate);
        await _usersRepository.AddAsync(userToCreate);
        await _rolesRepository.AssignToRoleAsync(userToCreate, Roles.User);
        await CompleteAndCheckIfCompleted();
    }
    public async Task<AuthResult> GetAuthCredentialsAsync(User user)
    {
        var foundUser = await _usersRepository.GetByNameAsync(user.Name);
        _passwordManager.ThrowExceptionIfWrongPassword(user.Password, foundUser.Password);
        var roleName = await _rolesRepository.GetRoleNameByIdAsync(foundUser.RoleId);
        var token = _tokenManager.GenerateToken(foundUser, roleName);
        return new AuthResult(foundUser, token, roleName);
    }
    public async Task UpdateUsernameAsync(User userInDb, User givenUser, string? newPassword)
    {
        _passwordManager.ThrowExceptionIfWrongPassword(givenUser.Password, userInDb.Password);
        userInDb.Name = givenUser.Name;
        SetNewPasswordIfPresent(userInDb, newPassword);
        await CompleteAndCheckIfCompleted();
    }
    private void SetNewPasswordIfPresent(User userInDb, string? newPassword)
    {
        if (newPassword != null)
            _passwordManager.SecureUserWithNewPassword(userInDb, newPassword);
    }

    public async Task DeleteAccountAsync(User user, string inputtedPassword)
    {
        _passwordManager.ThrowExceptionIfWrongPassword(inputtedPassword, user.Password);
        _usersRepository.Remove(user);
        await CompleteAndCheckIfCompleted();
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