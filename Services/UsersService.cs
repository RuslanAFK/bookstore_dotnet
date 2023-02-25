using BookStoreServer.Core.Models;
using BookStoreServer.Core.Services;

namespace Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenManager _tokenManager;
    
    public UsersService(IUsersRepository usersRepository, IUnitOfWork unitOfWork, ITokenManager tokenManager)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _tokenManager = tokenManager;
    }
    public async Task<ListResponse<User>> GetUsersAsync(QueryObject queryObject)
    {
        return await _usersRepository.GetUsersAsync(queryObject);
    }
    
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _usersRepository.GetUserByIdAsync(userId);
    }
    
    public async Task<User?> GetUserByNameAsync(string username)
    {
        return await _usersRepository.GetUserByNameAsync(username);
    }
    
    public async Task<bool> RemoveUserAsync(User user)
    {
        _usersRepository.RemoveUser(user);
        return await IsCompleted();
    }
    
    public async Task<bool> RegisterAsync(User userToCreate)
    {
        // Hash password
        userToCreate.Password = BCrypt.Net.BCrypt.HashPassword(userToCreate.Password);
        // Create User
        _usersRepository.CreateUser(userToCreate);
        // Add user to role
        await _usersRepository.AddUserToRole(userToCreate, false);
        return await IsCompleted();
    }

    public async Task<AuthResult?> GetAuthResultAsync(User user)
    {
        // Find user in db
        var foundUser = await _usersRepository.GetFullUser(user);
        if (foundUser == null)
            return null;
        // Check password
        CheckPassword(user.Password, foundUser.Password);
        // Get Role
        var roleName = await _usersRepository.GetRoleById(foundUser.RoleId);
        // Get token
        var token = _tokenManager.GenerateToken(foundUser, roleName);
        return new AuthResult
        {
            Id = foundUser.Id,
            Username = foundUser.Name,
            Token = token,
            Role = roleName
        };
    }

    public async Task<bool> UpdateProfileAsync(User foundUser, User user, string? newPassword)
    {
        CheckPassword(user.Password, foundUser.Password);
        foundUser.Name = user.Name;
        if (newPassword != null)
            foundUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        return await IsCompleted();
    }
    
    public async Task<bool> DeleteAccountAsync(User user, string inputtedPassword)
    {
        CheckPassword(inputtedPassword, user.Password);
        _usersRepository.RemoveUser(user);
        return await IsCompleted();
    }

    public async Task<bool> AddUserToRoleAsync(User user, bool isAdmin)
    {
        await _usersRepository.AddUserToRole(user, isAdmin);
        return await IsCompleted();
    }
    private async Task<bool> IsCompleted()
    {
        return await _unitOfWork.CompleteAsync() > 0;
    }

    private void CheckPassword(string realPassword, string hashedPassword)
    {
        if (!BCrypt.Net.BCrypt.Verify(realPassword, hashedPassword)) 
            throw new InvalidDataException("Provided incorrect password.");
    }
}