using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UsersRepository : SearchableRepository<User>, IUsersRepository
{
    public UsersRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> GetByIdIncludingRolesAsync(int id)
    {
        var users = GetUsersIncludingRoles();
        return await GetByIdAsync(id, users);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var users = GetAll();
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
    public IQueryable<User> GetUsersIncludingRoles()
    {
        var items = GetAll();
        return GetItemsIncluding(items, user => user.Role);
    }
    public new async Task AddAsync(User item)
    {
        await ThrowIfUserAlreadyFound(item.Name);
        await base.AddAsync(item);
    }
    private async Task ThrowIfUserAlreadyFound(string username)
    {
        var foundUser = await GetAll()
            .SingleOrDefaultAsync(user => user.Name == username);
        if (foundUser is not null)
            throw new EntityAlreadyExistsException(typeof(User), nameof(User.Name), username);
    }

}

