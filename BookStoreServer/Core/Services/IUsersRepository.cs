using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IUsersRepository
{
    Task<ListResponse<User>> GetUsersAsync(QueryObject queryObject);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByNameAsync(string username);
    void RemoveUser(User user);
    void Signup(User userToCreate);
    Task<User?> CheckCredentialsAsync(User userToLogin);
    Task<string> GetRoleById(int roleId);
    Task AddUserToRole(User user, bool isAdmin);
}