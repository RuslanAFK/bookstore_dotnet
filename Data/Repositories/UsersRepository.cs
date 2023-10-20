using Data.Abstractions;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    public UsersRepository(AppDbContext context) : base(context)
    {
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

    public async Task<bool> CreatorExists()
    {
        return await GetAll()
            .AsNoTracking()
            .AnyAsync(x => x.Role!.RoleName == Roles.Creator);
    }

    public new async Task AddAsync(User item)
    {
        await CheckIfUserExists(item.Name);
        await base.AddAsync(item);
    }

    private async Task CheckIfUserExists(string username)
    {
        var foundUser = await GetAll().GetByNameOrDefaultAsync(username);
        if (foundUser is not null)
            throw new EntityAlreadyExistsException(typeof(User), nameof(User.Name), username);
    }
}

