using Domain.Models;

namespace Domain.Abstractions;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<ListResponse<User>> GetQueriedItemsAsync(Query query);
    Task<User> GetByIdIncludingRolesAsync(int id);
    Task<User> GetByNameAsync(string name);
    Task<User> GetByNameIncludingRolesAsync(string name);
}

