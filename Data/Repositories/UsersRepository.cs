using Domain.Abstractions;
using Domain.Models;

namespace Data.Repositories;

public class UsersRepository : SearchableRepository<User>, IUsersRepository
{
    public UsersRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ListResponse<User>> GetQueriedItemsAsync(Query query)
    {
        var users = GetUsersIncludingRoles();
        return await GetQueriedItemsAsync(query, users);
    }
    public async Task<User> GetByIdIncludingRolesAsync(int id)
    {
        var users = GetUsersIncludingRoles();
        return await GetByIdAsync(id, users);
    }
    public async Task<User> GetByNameAsync(string name)
    {
        var users = GetAll();
        return await GetByNameAsync(name, users);
    }
    public async Task<User> GetByNameIncludingRolesAsync(string name)
    {
        var users = GetUsersIncludingRoles();
        return await GetByNameAsync(name, users);
    }
    private IQueryable<User> GetUsersIncludingRoles()
    {
        var items = GetAll();
        return GetItemsIncluding(items, user => user.Role);
    }

}

