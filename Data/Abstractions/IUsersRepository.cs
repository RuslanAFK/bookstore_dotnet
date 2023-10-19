using Domain.Models;

namespace Data.Abstractions;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByNameAsync(string name);
    Task<User> GetByIdIncludingRolesAsync(int id);
    Task<User> GetByNameIncludingRolesAsync(string name);
}

