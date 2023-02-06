using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IAuthRepository
{
    void Signup(User userToCreate);
    Task<User?> CheckCredentialsAsync(User userToLogin);
    Task<string> GetUserRole(int roleId);
    Task AddToRole(User user, bool isAdmin);
}