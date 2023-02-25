using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IUsersService
{
    Task<ListResponse<User>> GetUsersAsync(QueryObject queryObject);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByNameAsync(string username);
    Task<bool> RemoveUserAsync(User user);
    Task<bool> RegisterAsync(User userToCreate);
    Task<AuthResult?> GetAuthResultAsync(User user);
    Task<bool> UpdateProfileAsync(User foundUser, User user, string? newPassword);
    Task<bool> DeleteAccountAsync(User user, string inputtedPassword);
    Task<bool> AddUserToRoleAsync(User user, bool isAdmin);
}