using BookStoreServer.Core.Models;

namespace BookStoreServer.Core;

public interface IUsersRepository
{
    void Signup(User userToCreate);
    Task<User?> LoginAsync(User userToLogin);
}