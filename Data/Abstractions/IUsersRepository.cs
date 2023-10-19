using Domain.Models;

namespace Data.Abstractions;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User> GetByIdIncludingRolesAsync(int id);
    Task<User> GetByNameAsync(string name);
    Task<User> GetByNameIncludingRolesAsync(string name);
    IQueryable<User> GetUsersIncludingRoles();
}

