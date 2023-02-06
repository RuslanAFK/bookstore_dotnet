using BookStoreServer.Core.Models;

namespace BookStoreServer.Core.Services;

public interface IUsersRepository
{
    Task<ListResponse<User>> GetUsersAsync(QueryObject queryObject);
    Task<User?> GetUserByIdAsync(int userId);
    void UpdateUser(User user);
    void RemoveUser(User user);
}