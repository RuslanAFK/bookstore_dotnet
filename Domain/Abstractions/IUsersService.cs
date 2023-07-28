using Domain.Models;

namespace Domain.Abstractions;

public interface  IUsersService
{
    Task<ListResponse<User>> GetQueriedAsync(Query query);
    Task<User> GetByIdAsync(int userId);
    Task<User> GetByNameAsync(string username);
    Task RemoveAsync(User user);
    Task AddUserToRoleAsync(User user, string roleName);
}