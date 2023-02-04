using BookStoreServer.Core.Models;

namespace BookStoreServer.Core;

public interface IUsersRepository
{
    void Signup(User userToCreate);
    Task<User?> CheckCredentialsAsync(User userToLogin);
    Task<string> GetUserRole(int roleId);
    Task AddToRole(User user, bool isAdmin);
}