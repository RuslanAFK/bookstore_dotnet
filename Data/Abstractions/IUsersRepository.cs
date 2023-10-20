using Domain.Models;

namespace Data.Abstractions;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByNameAsync(string name);
    Task<bool> CreatorExists();
}

