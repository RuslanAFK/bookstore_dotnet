using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    public UsersRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> GetByIdIncludingRolesAsync(int id)
    {
        var users = GetUsersIncludingRoles();
        return await users.GetByIdAsync(id);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var users = GetAll();
        return await users.GetByIdAsync(id);
    }

    public async Task<User> GetByNameAsync(string name)
    {
        var users = GetAll();
        return await users.GetByNameAsync(name);
    }
    public async Task<User> GetByNameIncludingRolesAsync(string name)
    {
        var users = GetUsersIncludingRoles();
        return await users.GetByNameAsync(name);
    }
    public new async Task AddAsync(User item)
    {
        await CheckIfUserExists(item.Name);
        await base.AddAsync(item);
    }
    private IQueryable<User> GetUsersIncludingRoles()
    {
        return GetAll().Include(x => x.Role);
    }
    private async Task CheckIfUserExists(string username)
    {
        var foundUser = await GetAll().GetByNameOrDefaultAsync(username);
        if (foundUser is not null)
            throw new EntityAlreadyExistsException(typeof(User), nameof(User.Name), username);
    }
}

